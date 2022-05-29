using UnityEngine;
using TMPro;

public class ReverseTimer : MonoBehaviour
{   
    private float endTime;
    private float deltaTime = 90f;

    void Start()
    {
        this.endTime = Time.time + this.deltaTime;
    }

    void Update()
    {
        float remainingTime = this.endTime - Time.time;
        if(remainingTime < 0f) {
            remainingTime = 0f;
        }
        TMP_Text textMeshPro = this.GetComponent<TMP_Text>();
        textMeshPro.SetText(remainingTime.ToString("f4"));
    }
}
