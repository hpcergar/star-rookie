using UnityEngine.SceneManagement;
using UnityEngine;

public class IntroController : MonoBehaviour
{
    void Update()
    {
        if(Input.anyKey) {
            this.GoToLevel();
        }
    }

    public void GoToLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int nextSceneIndex = currentScene.buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
