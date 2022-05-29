using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherShipIntroCameraManager : MonoBehaviour
{
    [SerializeField]
    private Camera playerCamera;
    [SerializeField]
    private Camera civiliansCamera;
    [SerializeField]
    private Camera motherShipCamera;

    public void EnableCiviliansCamera()
    {
        this.playerCamera.enabled = false;
        this.civiliansCamera.enabled = true;
    }

    public void EnableMotherShipCamera()
    {
        this.civiliansCamera.enabled = false;
        this.motherShipCamera.enabled = true;
    }
    
    public void EnableMainCamera()
    {
        this.motherShipCamera.enabled = false;
        this.playerCamera.enabled = true;
    }
}
