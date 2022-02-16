using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    private int score = 0;
    
    public void IncreaseScore(int quantity)
    {
        this.score += quantity;
        Debug.Log(this.score);
    }
}
