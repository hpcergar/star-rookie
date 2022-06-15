using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    private ParticleSystem currentParticleSystem;

    // Start is called before the first frame update
    void Start()
    {
        this.currentParticleSystem = GetComponent<ParticleSystem>();
        if(this.currentParticleSystem == null) {
            this.currentParticleSystem = this.GetComponentInChildren<ParticleSystem>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(this.currentParticleSystem == null) {
            return;
        }

        if (false == this.currentParticleSystem.isPlaying)
        {
            Destroy(this.gameObject);
        }
    }
}
