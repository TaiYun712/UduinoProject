using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public Image clockTimer;

    float gameTimeRemaining;
    public float gameMaxTime = 180.0f;

    public Image scoreLiner;
    public Image yourScore;
    public Sprite good, bad;

    public AudioSource goodSound, badSound;

    public Animator gameOverBoard;
    public Button nextBt;

    void Start()
    {
        gameTimeRemaining = gameMaxTime;

        nextBt.onClick.AddListener(BackToTitle);
    }

    
    void Update()
    {
        if(gameTimeRemaining > 0)
        {
            gameTimeRemaining -= Time.deltaTime;
            clockTimer.fillAmount = gameTimeRemaining / gameMaxTime;
        }

        if(gameTimeRemaining < 0)
        {
            gameOverBoard.SetTrigger("gameover");

            if(scoreLiner.fillAmount > 0.5)
            {
                yourScore.sprite = good;
                goodSound.Play();
            }
            else
            {
                yourScore.sprite= bad;
                badSound.Play();
            }

            Invoke("Over", 1f);


        }
    }

    void Over()
    {
        Time.timeScale = 0;
    }

    void BackToTitle()
    {
        SceneManager.LoadScene("Start");
    }



}
