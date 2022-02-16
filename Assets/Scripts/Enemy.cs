using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] Transform parent;
    [SerializeField] int healthPoints = 10;
    [SerializeField] int scorePerHit = 10;
    private int damagePerHit = 10;

    ScoreBoard scoreBoard;

    private void Start() 
    {
        this.scoreBoard = FindObjectOfType<ScoreBoard>();    
    }

    private void OnParticleCollision(GameObject other) 
    {
        this.ProcessHit();
        if(this.healthPoints > 0) {
            HitEnemy(other);
        } else {
            KillEnemy();
        }
    }

    private void ProcessHit()
    {
        this.healthPoints -= damagePerHit;
        this.scoreBoard.IncreaseScore(this.scorePerHit);
    }

    private void HitEnemy(GameObject other)
    {
        ParticleSystem particleSystem = other.GetComponent<ParticleSystem>();
        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
        particleSystem.GetCollisionEvents(this.gameObject, collisionEvents);

        foreach(var collisionEvent in collisionEvents)
        {
            this.displayHitVfx(collisionEvent.intersection);
        }
    }

    private void displayHitVfx(Vector3 position)
    {
        GameObject vfx = Instantiate(hitVFX, position, Quaternion.identity);
        vfx.transform.parent = this.parent;
        vfx.AddComponent<SelfDestruct>();
    }

    private void KillEnemy()
    {
        GameObject vfx = Instantiate(deathVFX, this.transform.position, Quaternion.identity);
        vfx.transform.parent = this.parent;
        vfx.AddComponent<SelfDestruct>();
        Destroy(this.gameObject);    
    }
}
