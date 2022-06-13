using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] float startOffset = 0f;

    private void Start()
    {
        this.GetComponent<AudioSource>().time += this.startOffset;
    }
}
