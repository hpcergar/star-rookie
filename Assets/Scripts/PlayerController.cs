using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("How fast ship moves on input")][SerializeField] float controlSpeed = 80f;
    [Tooltip("How wide the ship can move")][SerializeField] float xRange = 12f;
    [Tooltip("How high the ship can move")][SerializeField] float yRange = 7f;
    [Tooltip("Ship height movement compensation")][SerializeField] float yExtraOffset = 3f;

    [Header("Laser gun array")]
    [Tooltip("Add all player lasers here")]
    [SerializeField] GameObject[] lasers;

    [Header("Screen position tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = 1.5f;

    [Header("Player input tuning")]
    [SerializeField] float controlRollFactor = -20f;
    [SerializeField] float controlPitchFactor = 15f;
    float startDelay = 2f;
    bool isStarted = false;
    

    private void Start()
    {
        Invoke("StartControls", startDelay);
        ToggleLasers(false);
    }

    private void StartControls()
    {
        isStarted = true;
    }

    void Update()
    {
        if(false == isStarted) {
            return;
        }
        
        float xThrow = Input.GetAxis("Horizontal");
        float yThrow = Input.GetAxis("Vertical");
        ProcessTranslation(xThrow, yThrow);
        ProcessRotation(xThrow, yThrow);
        ProcessFiring();
    }
    
    private void ProcessRotation(float xThrow, float yThrow)
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;

        float pitch = pitchDueToPosition - pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation(float xThrow, float yThrow)
    {
        Vector3 localPosition = transform.localPosition;
        transform.localPosition = new Vector3(
            TransformInputToOffset(localPosition.x, xThrow, -xRange, xRange),
            TransformInputToOffset(localPosition.y, yThrow, -yRange + yExtraOffset, yRange + yExtraOffset),
            localPosition.z
        );
    }

    private float TransformInputToOffset(float position, float axisThrow, float min, float max)
    {
        return Mathf.Clamp(position + (controlSpeed * Time.deltaTime * axisThrow), min, max);
    }

    private float AdjustInTime(float value)
    {
        return Time.deltaTime * value;
    }

    private void ProcessFiring()
    {
        bool isFiring = Input.GetButton("Fire1");
        ToggleLasers(isFiring);
    }

    private void ToggleLasers(bool active)
    {
        foreach(GameObject laser in lasers) {
            var emmission = laser.GetComponent<ParticleSystem>().emission;
            emmission.enabled = active;
        }
    }
}
