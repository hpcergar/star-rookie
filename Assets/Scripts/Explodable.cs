using System.Collections.Generic;
using UnityEngine;

public class Explodable : MonoBehaviour
{
    [SerializeField] GameObject deathFX;

    // Cache
    GameObject parentGameObject;

    private void Start() 
    {
        this.parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
        this.AddRigidbody();
    }

    private void AddRigidbody()
    {
        Rigidbody currentRigidbody = this.gameObject.AddComponent<Rigidbody>();  
        currentRigidbody.useGravity = false;
        currentRigidbody.isKinematic = true;
    }

    private void OnParticleCollision() 
    {
        this.KillCivilian();
    }

    private void KillCivilian()
    {
        GameObject fx = Instantiate(deathFX, this.transform.position, Quaternion.identity);
        fx.transform.parent = this.parentGameObject.transform;
        fx.AddComponent<SelfDestruct>();
        MeshRenderer meshRenderer = this.gameObject.GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
        Destroy(this.gameObject);
    }
}
