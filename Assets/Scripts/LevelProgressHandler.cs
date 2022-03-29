using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelProgressHandler : MonoBehaviour
{
    // TODO Checkpoints handling

    public void ReloadLevelAfter(float delay)
    {
        if(0f == delay) {
            this.ReloadLevel();
            return;
        }

        Invoke("ReloadLevel", delay);
    }

    public void ReloadLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
