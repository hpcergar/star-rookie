using UnityEngine.SceneManagement;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] ParticleSystem explosionParticleSystem;
    LevelProgressHandler levelProgressHandler;

    private void Start()
    {
        this.explosionParticleSystem.Stop();
        this.levelProgressHandler = FindObjectOfType<LevelProgressHandler>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        StartCrashSequance();
    }

    private void StartCrashSequance()
    {
        this.GetComponent<PlayerController>().enabled = false;
        this.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<BoxCollider>().enabled = false;
        this.explosionParticleSystem.Play();

        Invoke("ReloadLevel", 1f);
    }

    private void ReloadLevel()
    {
        this.levelProgressHandler.ReloadLevel();
    }
}
