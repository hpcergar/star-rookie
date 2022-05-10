using UnityEngine;

public class MotherShip : MonoBehaviour
{
    public void NotifyWeakPointHasBeenDestroyed()
    {
        int remainingWeakPointCount = GameObject.FindGameObjectsWithTag("MotherShipWeakPoint").Length;
        if(remainingWeakPointCount <= 0) {
            Debug.Log("Destroyed");
            // TODO launch cutscene
        }
    }
}
