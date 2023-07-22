using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI Instance;
    public Image healthBar, manaBar, score1Bar, score2Bar;
    int score1, maxScore1, score2, maxScore2;
    [NonSerialized] public bool taskComplete;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        gameObject.SetActive(SceneManager.GetActiveScene().buildIndex != 0);
    }
    private void Start()
    {
        maxScore1 = GameObject.FindGameObjectsWithTag("Score1").Length;
        maxScore2 = GameObject.FindGameObjectsWithTag("Enemy").Length + GameObject.FindGameObjectsWithTag("Score2").Length;
        if (maxScore1 == 0)
            score1Bar.fillAmount = 1;
        if (maxScore2 == 0)
            score2Bar.fillAmount = 1;
    }

    private void FixedUpdate()
    {
        if (maxScore1 != 0)
            score1Bar.fillAmount = (float)score1 / (float)maxScore1;
        if (maxScore2 != 0)
            score2Bar.fillAmount = (float)score2 / (float)maxScore2;
        if (score1Bar.fillAmount == 1 && score2Bar.fillAmount == 1)
            taskComplete = true;
    }

    public void IncreaseScore(string score)
    {
        if (score == "Score1")
            score1++;
        if (score == "Score2")
            score2++;
    }

    public void Pause()
    {
        FindAnyObjectByType<Setting>(FindObjectsInactive.Include).Pause();
    }
}
