using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    private ParticleSystem currentParticleSystem;

    // Start is called before the first frame update
    void Start()
    {
        currentParticleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (false == currentParticleSystem.isPlaying)
        {
            Destroy(this.gameObject);
        }
    }
}
