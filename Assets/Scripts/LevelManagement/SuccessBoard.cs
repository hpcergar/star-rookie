using UnityEngine;
using TMPro;

public class SuccessBoard : MonoBehaviour
{
    private TMP_Text text;

    private void Awake() 
    {
        this.text = GetComponent<TMP_Text>();
        int playerScore  =  PlayerPrefs.GetInt("score");
        int playerDeaths  =  PlayerPrefs.GetInt("deaths");
        this.ResetUserStats();
        text.SetText("Final score: " + playerScore.ToString() 
        + "\n" + "Deaths: " + playerDeaths.ToString()
        + "\n" + "Congratulations!"
        );
    }

    private void ResetUserStats()
    {
        PlayerPrefs.SetInt("score", 0);
        PlayerPrefs.SetInt("deaths", 0);
    }
}
