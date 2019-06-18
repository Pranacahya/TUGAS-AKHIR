using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    int totalScore;
    int counter;
    public void SetTotalScore(int score)
    {
        this.totalScore = score;
        counter++;
    }

    public int GetTotalScore()
    {
        return this.totalScore/counter;
    }
}
