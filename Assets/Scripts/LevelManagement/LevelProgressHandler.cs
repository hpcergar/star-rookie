using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Playables;

public class LevelProgressHandler : MonoBehaviour
{

    // Checkpoints handling
    [SerializeField]
    private int checkpointIndex = 0;
    private float[] checkpoints = new float[4]
    {
        0f,
        40.0f,
        77.0f,
        127.0f
    };

    private void Awake()
    {
        int instancesCount = FindObjectsOfType<LevelProgressHandler>().Length;
        if (instancesCount > 1)
        {
            Destroy(this.gameObject);
            return;
        }
        
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.Play();
    }

    private void Play()
    {
        PlayableDirector playableDirector = GameObject.FindWithTag("MasterTimeline").GetComponent<PlayableDirector>();
        float delay = this.checkpoints[this.checkpointIndex];
        delay = delay + (float)playableDirector.initialTime;
        playableDirector.time = delay;
        playableDirector.Play();
    }

    public void ReloadLevelAfter(float delay)
    {
        if(0f == delay) {
            this.ReloadLevel();
            return;
        }

        Invoke("ReloadLevel", delay);
    }

    public void SetCheckpointIndex(int index)
    {
        this.checkpointIndex = index;
    }

    public void ReloadLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);        
    }
}
