using System.Collections.Generic;
using UnityEngine;

public class MotherShipWeakPoint : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] int healthPoints = 50;
    [SerializeField] int scorePerHit = 10;

    private int damagePerHit = 10;
    ScoreBoard scoreBoard;

    // Cache
    GameObject parentGameObject;

    private void Start()
    {
        this.scoreBoard = FindObjectOfType<ScoreBoard>();
        this.parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
        this.AddRigidbody();
    }

    private void AddRigidbody()
    {
        Rigidbody currentRigidbody = this.gameObject.AddComponent<Rigidbody>();
        currentRigidbody.useGravity = false;
        currentRigidbody.isKinematic = true;
    }

    private void OnParticleCollision(GameObject other)
    {
        this.ProcessHit();
        if (this.healthPoints > 0)
        {
            Hit(other);
        }
        else
        {
            DestroyWeakPoint();
        }
    }

    private void ProcessHit()
    {
        this.healthPoints -= damagePerHit;
    }

    private void Hit(GameObject other)
    {
        ParticleSystem particleSystem = other.GetComponent<ParticleSystem>();
        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
        particleSystem.GetCollisionEvents(this.gameObject, collisionEvents);

        foreach (var collisionEvent in collisionEvents)
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

    private void DestroyWeakPoint()
    {
        this.scoreBoard.IncreaseScore(this.scorePerHit);
        GameObject fx = Instantiate(deathFX, this.transform.position, Quaternion.identity);
        fx.transform.parent = this.parentGameObject.transform;
        fx.AddComponent<SelfDestruct>();
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        this.NotifyWeakPointHasBeenDestroyed();
    }

    private void NotifyWeakPointHasBeenDestroyed()
    {
        GameObject motherShip = GameObject.FindWithTag("MotherShip");
        if(null == motherShip) {
            return;
        }
        MotherShip motherShipScript = motherShip.GetComponent<MotherShip>();
        if(null != motherShipScript) {
            motherShipScript.NotifyWeakPointHasBeenDestroyed(this.gameObject.GetInstanceID());
        }
    }
}
