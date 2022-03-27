using System.Collections.Generic;
using UnityEngine;

public class Civilian : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] int healthPoints = 10;
    [SerializeField] int scorePerHit = 10;
    GameObject parentGameObject;
    private int damagePerHit = 10;

    ScoreBoard scoreBoard;
    LevelProgressHandler levelProgressHandler;

    private void Start() 
    {
        this.scoreBoard = FindObjectOfType<ScoreBoard>();
        this.levelProgressHandler = FindObjectOfType<LevelProgressHandler>();
        this.parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
        this.AddRigidbody();
    }

    private void AddRigidbody()
    {
        Rigidbody currentRigidbody = this.gameObject.AddComponent<Rigidbody>();  
        currentRigidbody.useGravity = false;
    }

    private void OnParticleCollision(GameObject other) 
    {
        this.ProcessHit();
        if(this.healthPoints > 0) {
            HitCivilian(other);
        } else {
            KillCivilian();
        }
    }

    private void ProcessHit()
    {
        this.healthPoints -= damagePerHit;
    }

    private void HitCivilian(GameObject other)
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

    private void KillCivilian()
    {
        this.scoreBoard.DecreaseScore(this.scorePerHit);
        GameObject fx = Instantiate(deathFX, this.transform.position, Quaternion.identity);
        fx.transform.parent = this.parentGameObject.transform;
        fx.AddComponent<SelfDestruct>();
        MeshRenderer meshRenderer = this.gameObject.GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
        Invoke("ReloadLevel", 1f);
    }

    private void ReloadLevel()
    {
        this.levelProgressHandler.ReloadLevel();
    }
}
