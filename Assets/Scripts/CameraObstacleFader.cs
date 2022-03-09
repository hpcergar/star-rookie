using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObstacleFader : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "CameraColliderTrigger")
        {
            Destroy(this.gameObject);
        }
    }
}
