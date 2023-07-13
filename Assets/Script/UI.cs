using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Image healthBar, manaBar, score1Bar, score2Bar;
    int score1, maxScore1, score2, maxScore2;
    private void Start()
    {
        maxScore1 = GameObject.FindGameObjectsWithTag("Score1").Length;
        maxScore2 = GameObject.FindGameObjectsWithTag("Score2").Length;
    }

    private void FixedUpdate()
    {
        score1Bar.fillAmount = (float)score1 / (float)maxScore1;
        score2Bar.fillAmount = (float)score2 / (float)maxScore2;
    }

    public void IncreaseScore(string score)
    {
        if (score == "Score1")
            score1++;
        if (score == "Score2")
            score2++;
    }

}
