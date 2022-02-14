using UnityEngine.SceneManagement;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] ParticleSystem explosionParticleSystem;

    private void Start()
    {
        explosionParticleSystem.Stop();
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
        explosionParticleSystem.Play();

        Invoke("ReloadLevel", 1f);
    }

    private void ReloadLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
