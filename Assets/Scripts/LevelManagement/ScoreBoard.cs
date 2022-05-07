using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    private int score = 0;
    private TMP_Text text;

    private void Start() 
    {
        this.text = GetComponent<TMP_Text>();
        text.SetText("Start!");
    }
    
    public void IncreaseScore(int quantity)
    {
        this.score += quantity;
        refreshUI();
    }

    public void DecreaseScore(int quantity)
    {
        this.score -= quantity;
        refreshUI();
    }

    private void refreshUI()
    {
        text.SetText(score.ToString());
    }
}
