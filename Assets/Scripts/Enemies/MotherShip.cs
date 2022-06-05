using System.Collections.Generic;
using UnityEngine;

public class MotherShip : MonoBehaviour
{
    // Cache
    private GameObject frontWeakPointsGroup;
    private GameObject rearWeakPointsGroup;
    private Dictionary<int, GameObject> weakPoints = new Dictionary<int, GameObject>();

    private void OnEnable()
    {
        CacheCollidersAndWeakPoints();
    }

    private void CacheCollidersAndWeakPoints()
    {
        var hitColliders = this.gameObject.transform.Find("Hit colliders");
        if (hitColliders)
        {
            this.CacheColliders();
            this.CacheWeakPoints();
        }
    }

    private void CacheColliders()
    {
        var colliders = this.gameObject.transform.Find("Hit colliders").gameObject;
        this.frontWeakPointsGroup = colliders.transform.Find("Front").gameObject;
        this.rearWeakPointsGroup = colliders.transform.Find("Rear").gameObject;
    }

    private void CacheWeakPoints()
    {
        this.AddCacheWeakPointsGroup(this.frontWeakPointsGroup);
        this.AddCacheWeakPointsGroup(this.rearWeakPointsGroup);
    }

    private void AddCacheWeakPointsGroup(GameObject weakPointsGroup)
    {
        foreach (Transform weakPoint in weakPointsGroup.transform)
        {
            this.weakPoints.Add(weakPoint.gameObject.GetInstanceID(), weakPoint.gameObject);
            Debug.Log("Added weakpoint with ID " + weakPoint.gameObject.GetInstanceID().ToString() + " with name " + weakPoint.gameObject.name);
        }
    }

    public void NotifyWeakPointHasBeenDestroyed(int destroyedWeakPointInstanceID)
    {
        if (false == this.weakPoints.ContainsKey(destroyedWeakPointInstanceID))
        {
            return;
        }

        this.weakPoints.Remove(destroyedWeakPointInstanceID);
        int remainingWeakPointCount = this.weakPoints.Count;
        if (remainingWeakPointCount <= 0)
        {
            LevelProgressHandler levelProgressHandler = FindObjectOfType<LevelProgressHandler>();
            levelProgressHandler.SuccessLevel();
        }
    }

    public void EnableFrontWeakPoints()
    {
        this.frontWeakPointsGroup.SetActive(true);
        this.rearWeakPointsGroup.SetActive(false);
    }

    public void EnableRearWeakPoints()
    {
        this.frontWeakPointsGroup.SetActive(false);
        this.rearWeakPointsGroup.SetActive(true);
    }

    public void DisableAllWeakPoints()
    {
        this.frontWeakPointsGroup.SetActive(false);
        this.rearWeakPointsGroup.SetActive(false);
    }
}
