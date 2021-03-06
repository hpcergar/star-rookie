using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBoundTrigger : MonoBehaviour
{
    [SerializeField] GameObject vfxPrefab;

    GameObject vfxInstance;
    GameObject vfxParent;
    Camera cameraInstance;
    Transform currentTransform;
    bool wasVisible = false;

    private void Start()
    {
        this.cameraInstance = Camera.main;
        this.currentTransform = this.gameObject.GetComponent<Transform>();
        this.vfxParent = GameObject.FindWithTag("VfxSpawnAtRuntime");
    }

    private void Update()
    {
        bool isCurrentlyVisible = this.IsVisible();
        if (this.HasBecomeVisible(isCurrentlyVisible))
        {
            this.wasVisible = true;
            this.CreateVfxInstance();
            this.AttachVfxToParent(this.vfxPrefab);
        }
        else if (this.HasBecomeInvisible(isCurrentlyVisible))
        {
            this.wasVisible = false;
            this.DetachVfxFromParent();
        }
    }

    private void OnDestroy()
    {
        this.DestroyVFXInstance();
    }

    private void OnDisable() {
        this.DestroyVFXInstance();
    }

    private void DestroyVFXInstance()
    {
        if(null != this.vfxInstance) {
            Destroy(this.vfxInstance);
        }
    }

    private void CreateVfxInstance()
    {
        if (null == this.vfxInstance)
        {
            this.vfxInstance = Instantiate(vfxPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            TargetVFX targetVfx = this.vfxInstance.GetComponent<TargetVFX>();
            targetVfx.setTargetBound(this.gameObject);
        }
    }

    private void AttachVfxToParent(GameObject vfxPrefab)
    {
        this.vfxInstance.transform.parent = this.vfxParent.transform;
    }

    private void DetachVfxFromParent()
    {
        if (null == this.vfxInstance)
        {
            return;
        }
        this.vfxInstance.transform.parent = null;
    }

    private bool IsVisible()
    {
        Vector3 position = this.cameraInstance.WorldToViewportPoint(this.currentTransform.position);
        return position.x > 0 && position.x < 1 && position.y > 0 && position.y < 1 && position.z > 0;
    }

    private bool HasBecomeVisible(bool isVisible)
    {
        return false == this.wasVisible && isVisible;
    }

    private bool HasBecomeInvisible(bool isVisible)
    {
        return true == this.wasVisible && false == isVisible;
    }
}
