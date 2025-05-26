using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;
using UnityEngine.UI;

public class ReplyMessage : MonoBehaviour
{
    UduinoManager u;

    public int phonePin = 3;

    public Image replyLiner; //回覆訊息的進度條
    public Image replyTimer;//回復限時

    public GameObject myMessages;

    float timeRemaining;
    public float maxTime = 10.0f;

    public Animator phoneScreen;

    bool touchDetected = false; //是否按著

    float step;//進度條每次減少的份量
    void Start()
    {
        UduinoManager.Instance.pinMode(phonePin, PinMode.Input);

        int initialTouch = UduinoManager.Instance.digitalRead(phonePin);
        touchDetected = (initialTouch == 1);

        step = replyLiner.fillAmount / 15;


        timeRemaining = maxTime;

    }

    // Update is called once per frame
    void Update()
    {
        if(CamCtrl.lookDown == true)
        {
            //點擊回復訊息
            int touch = UduinoManager.Instance.digitalRead(phonePin);
           // Debug.Log("phone touch is" + touch);

            if (touch == 1 && !touchDetected)
            {
                replyLiner.fillAmount -= step;
                touchDetected = true;

                phoneScreen.SetBool("replying", true);
            }
            else if (touch == 0)
            {
                touchDetected = false;
                phoneScreen.SetBool("replying", false);

            }

        }

        //回復時限
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            replyTimer.fillAmount = timeRemaining / maxTime;
        }

        if (replyLiner.fillAmount == 0)
        {
            myMessages.SetActive(false);
           // Debug.Log("回復成功");
        }
        else if (timeRemaining < 0)
        {
            myMessages.SetActive(false);
           // Debug.Log("回復失敗");

        }


    }
}
