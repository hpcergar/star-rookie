using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    float time;

    // Update is called once per frame
    void Update()
    {
        this.time = Time.time;
        TMP_Text textMeshPro = this.GetComponent<TMP_Text>();
        textMeshPro.SetText(this.time.ToString("f4"));
    }
}
