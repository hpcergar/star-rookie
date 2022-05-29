using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherShipIntroCameraManager : MonoBehaviour
{
    [SerializeField]
    private Camera playerCamera;
    [SerializeField]
    private Camera civiliansCamera;

    public void EnableCiviliansCamera()
    {
        this.playerCamera.enabled = false;
        this.civiliansCamera.enabled = true;
    }

    public void EnableMainCamera()
    {
        this.civiliansCamera.enabled = false;
        this.playerCamera.enabled = true;
    }
}
