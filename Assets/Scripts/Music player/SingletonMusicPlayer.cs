using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SingletonMusicPlayer : MonoBehaviour
{
    [SerializeField] float startOffset = 0f;
    [SerializeField] bool loop = false;
    [SerializeField] float loopOffset = 0f;

    // Cache
    private AudioSource audioSource;

    private void Awake()
    {
        int musicPlayerCount = FindObjectsOfType<SingletonMusicPlayer>().Length;
        if (musicPlayerCount > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        this.audioSource = this.GetComponent<AudioSource>();
        this.audioSource.time = this.startOffset;
    }

    private void Update() 
    {
        if(this.audioSource.isPlaying) {
            return;
        }

        if(this.loop) {
            this.audioSource.time = this.loopOffset;
            this.audioSource.Play();
        }
    }
}
