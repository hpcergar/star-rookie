using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherShipTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject cameraManager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){
            MotherShipIntroCameraManager motherShipIntroCameraManager = cameraManager.GetComponent<MotherShipIntroCameraManager>();
            motherShipIntroCameraManager.EnableCiviliansCamera();
        }
    }
}
