using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] int healthPoints = 10;
    [SerializeField] int scorePerHit = 10;
    [SerializeField] float vulnerableAfter = 0;
    GameObject parentGameObject;
    private int damagePerHit = 10;
    private bool isInvulnerable = false;

    ScoreBoard scoreBoard;

    private void Start() 
    {
        this.scoreBoard = FindObjectOfType<ScoreBoard>();
        this.parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
        this.AddRigidbody();
        if(this.vulnerableAfter > 0f) {
            this.isInvulnerable = true;
            Invoke("SetVulnerable", this.vulnerableAfter);
        }
    }

    private void AddRigidbody()
    {
        Rigidbody currentRigidbody = this.gameObject.AddComponent<Rigidbody>();  
        currentRigidbody.useGravity = false;
    }

    private void OnParticleCollision(GameObject other) 
    {
        if(this.isInvulnerable) {
            return;
        }
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
    }

    private void HitEnemy(GameObject other)
    {
        ParticleSystem particleSystem = other.GetComponent<ParticleSystem>();
        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
        particleSystem.GetCollisionEvents(this.gameObject, collisionEvents);

        foreach(var collisionEvent in collisionEvents)
        {
            this.DisplayHitVfx(collisionEvent.intersection);
        }
    }

    private void DisplayHitVfx(Vector3 position)
    {
        GameObject vfx = Instantiate(hitVFX, position, Quaternion.identity);
        vfx.transform.parent = this.parentGameObject.transform;
        vfx.AddComponent<SelfDestruct>();
    }

    private void KillEnemy()
    {
        this.scoreBoard.IncreaseScore(this.scorePerHit);
        GameObject fx = Instantiate(deathFX, this.transform.position, Quaternion.identity);
        fx.transform.parent = this.parentGameObject.transform;
        fx.AddComponent<SelfDestruct>();
        Destroy(this.gameObject);    
    }

    private void SetVulnerable()
    {
        this.isInvulnerable = false;
    }
}
