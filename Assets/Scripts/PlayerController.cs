using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float controlSpeed = 80f;
    [SerializeField] float xRange = 12f;
    [SerializeField] float yRange = 7f;
    [SerializeField] float yExtraOffset = 3f;

    [SerializeField] float positionPitchFactor = -2f;    
    [SerializeField] float positionYawFactor = 1.5f;

    [SerializeField] float controlRollFactor = -20f;
    [SerializeField] float controlPitchFactor = 15f;
    


    void Update()
    {
        float xThrow = Input.GetAxis("Horizontal");
        float yThrow = Input.GetAxis("Vertical");
        processTranslation(xThrow, yThrow);
        processRotation(xThrow, yThrow);
    }

    private void processRotation(float xThrow, float yThrow)
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;

        float pitch = pitchDueToPosition - pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void processTranslation(float xThrow, float yThrow)
    {
        Vector3 localPosition = transform.localPosition;
        transform.localPosition = new Vector3(
            transformInputToOffset(localPosition.x, xThrow, -xRange, xRange),
            transformInputToOffset(localPosition.y, yThrow, -yRange + yExtraOffset, yRange + yExtraOffset),
            localPosition.z
        );
    }

    private float transformInputToOffset(float position, float axisThrow, float min, float max)
    {
        return Mathf.Clamp(position + (controlSpeed * Time.deltaTime * axisThrow), min, max);
    }

    private float adjustInTime(float value)
    {
        return Time.deltaTime * value;
    }
}
