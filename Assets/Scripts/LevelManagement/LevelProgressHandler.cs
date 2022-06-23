using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Playables;

public class LevelProgressHandler : MonoBehaviour
{
    // Checkpoints handling
    [SerializeField]
    private int checkpointIndex = 0;
    private int deaths = 0;
    private float[] checkpoints = new float[4]
    {
        0f,
        40.0f,
        77.0f,
        151.5f
    };

    // Cache
    private ScoreBoard scoreBoard;
    private GameObject musicPlayerGameObject;
    
    private void Start() 
    {
        this.scoreBoard = FindObjectOfType<ScoreBoard>();
        this.musicPlayerGameObject = FindObjectOfType<MusicPlayer>().gameObject;
    }

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
        GameObject masterTimeline = GameObject.FindWithTag("MasterTimeline");
        if(null == masterTimeline) {
            return;
        }

        PlayableDirector playableDirector = masterTimeline.GetComponent<PlayableDirector>();
        float delay = this.checkpoints[this.checkpointIndex];
        if (this.checkpointIndex == 0)
        {
            delay = delay + (float)playableDirector.initialTime;
        }
        playableDirector.time = delay;
        playableDirector.Play();
    }

    public void ReloadLevelAfter(float delay)
    {
        if (0f == delay)
        {
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

    public void IncreaseDeaths()
    {
        this.deaths++;
    }

    public void SuccessLevel()
    {
        PlayerPrefs.SetInt("score", this.scoreBoard.GetCurrentScore());
        PlayerPrefs.SetInt("deaths", this.deaths);
        if(this.musicPlayerGameObject) {
            Destroy(this.musicPlayerGameObject);
        }

        this.ResetProgress();
        SceneManager.LoadScene("Success scene");
    }

    private void ResetProgress()
    {
        this.checkpointIndex = 0;
        this.deaths = 0;
    }
}
