using UnityEngine.SceneManagement;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    private bool skipEnabled = false;

    void Start() 
    {
        Invoke("EnableSkip", 1f);
    }

    void Update()
    {
        if(skipEnabled && Input.GetButton("Fire1")) {
            this.GoToLevel();
        }
    }

    private void EnableSkip()
    {
        this.skipEnabled = true;
    }

    public void GoToLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int nextSceneIndex = currentScene.buildIndex + 1;

        if(nextSceneIndex >= SceneManager.sceneCountInBuildSettings) {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
