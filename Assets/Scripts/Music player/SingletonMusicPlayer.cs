using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMusicPlayer : MonoBehaviour
{
    [SerializeField] float startOffset = 0f;

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
        this.GetComponent<AudioSource>().time += this.startOffset;
    }
}
