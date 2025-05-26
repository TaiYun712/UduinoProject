using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Mrg1 : MonoBehaviour
{
    public Button startBt;
    public Button quitBt;

    public Animator startCam;
    public Animator fade;
    void Start()
    {
        startBt.onClick.AddListener(GameStart);

        quitBt.onClick.AddListener(QuitGame);

    }

   

    void GameStart()
    {
        startBt.gameObject.SetActive(false);
        quitBt.gameObject.SetActive(false);

        startCam.SetTrigger("gamestart");
        fade.SetTrigger("fade");

        Invoke("LoadGame", 3f);

    }

    void LoadGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void QuitGame()
    {
        Application.Quit();
    }


}
