using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelProgressHandler : MonoBehaviour
{
    // TODO Checkpoints handling

    

    public void ReloadLevel()
    {
        Debug.Log("Reloading level");
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
