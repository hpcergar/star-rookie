using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) 
    {
        Debug.Log("Collided: " + other.ToString() + " with " + this.ToString());    
    }

    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log($"Collided: {other.gameObject.name} with {this.name}");    
    }
}
