using UnityEngine;

public class MotherShip : MonoBehaviour
{
    // Cache
    private GameObject frontWeakPoints;
    private GameObject rearWeakPoints;

    private void Start() 
    {
        var colliders = this.gameObject.transform.Find("Hit colliders").gameObject;
        this.frontWeakPoints = colliders.transform.Find("Front").gameObject;
        this.rearWeakPoints = colliders.transform.Find("Rear").gameObject;
    }

    public void NotifyWeakPointHasBeenDestroyed()
    {
        int remainingWeakPointCount = GameObject.FindGameObjectsWithTag("MotherShipWeakPoint").Length;
        if(remainingWeakPointCount <= 0) {
            Debug.Log("Destroyed");
            // TODO launch cutscene
        }
    }

    public void EnableFrontWeakPoints()
    {
        this.frontWeakPoints.SetActive(true);
        this.rearWeakPoints.SetActive(false);
    }
    
    public void EnableRearWeakPoints()
    {
        this.frontWeakPoints.SetActive(false);
        this.rearWeakPoints.SetActive(true);
    }
}
