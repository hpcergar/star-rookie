using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetVFX : MonoBehaviour
{
    GameObject targetBound;
    Camera cameraInstance;
    Canvas canvas;
    RectTransform rectTransform;

    public void setTargetBound(GameObject targetBound)
    {
        this.targetBound = targetBound;
    }

    private void Start()
    {
        this.cameraInstance = Camera.main;
        this.rectTransform = this.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if(null == this.canvas){
            this.canvas = GetComponentInParent<Canvas>().rootCanvas;
        }
        if (false == this.IsReady())
        {
            return;
        }

        Vector3 canvasScale = this.canvas.transform.localScale;
        var rect = TargetVFX.GetRectFromObject(this.cameraInstance, this.targetBound);
        this.rectTransform.localScale = new Vector3(1 / canvasScale.x, 1 / canvasScale.y, 1 / canvasScale.z);
        this.rectTransform.anchoredPosition = new Vector2(rect.position.x * 1 / canvasScale.x, rect.position.y * 1 / canvasScale.y);
        this.rectTransform.sizeDelta = rect.size;
    }

    private bool IsReady()
    {
        return null != this.targetBound && null != this.canvas;
    }

    public static Rect GetRectFromObject(Camera camera, GameObject target)
    {
        var renderer = target.GetComponent<Renderer>();
        Vector3 center = renderer.bounds.center;
        Vector3 extent = renderer.bounds.extents;
        Vector2[] extentPoints = new Vector2[8]
        {
            camera.WorldToScreenPoint(new Vector3(center.x - extent.x, center.y - extent.y, center.z - extent.z)),
            camera.WorldToScreenPoint(new Vector3(center.x + extent.x, center.y - extent.y, center.z - extent.z)),
            camera.WorldToScreenPoint(new Vector3(center.x - extent.x, center.y - extent.y, center.z + extent.z)),
            camera.WorldToScreenPoint(new Vector3(center.x + extent.x, center.y - extent.y, center.z + extent.z)),
            camera.WorldToScreenPoint(new Vector3(center.x - extent.x, center.y + extent.y, center.z - extent.z)),
            camera.WorldToScreenPoint(new Vector3(center.x + extent.x, center.y + extent.y, center.z - extent.z)),
            camera.WorldToScreenPoint(new Vector3(center.x - extent.x, center.y + extent.y, center.z + extent.z)),
            camera.WorldToScreenPoint(new Vector3(center.x + extent.x, center.y + extent.y, center.z + extent.z))
        };

        Vector2 min = extentPoints[0];
        Vector2 max = extentPoints[0];
        foreach (Vector2 v in extentPoints)
        {
            min = new Vector2(Mathf.Min(min.x, v.x), Mathf.Min(min.y, v.x));
            max = new Vector2(Mathf.Max(max.x, v.x), Mathf.Max(max.y, v.y));
        }

        return new Rect(min.x, min.y, max.x - min.x, max.y - min.y);
    }
}
