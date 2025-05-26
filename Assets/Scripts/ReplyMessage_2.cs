using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;
using UnityEngine.UI;

public class ReplyMessage_2 : MonoBehaviour
{
    public AudioSource messageSound;
    bool soundPlayed = false;

    UduinoManager u;

    //女友頭像
    public Image herAvatar;
    public Sprite happyFace, sadFace;

    public int phonePin = 3;
    bool touchDetected = false; //是否按著


    public Image replyLiner; //回覆訊息的進度條
    

    public Animator phoneScreen; //手機螢幕動畫
    //常態需要的數值

    public float lessTime = 60.0f; //遊戲時長
    float normalStep; //一般情況下的減少
    float normalPlusStep; //一般的回覆增量

    //緊急訊息需要的數值
    float timeRemaining;
    float maxTime = 10.0f;
    
   

    public Image replyTimer;//回復時限
    public Image urgentLiner; //緊急訊息的進度條
    public GameObject urgentMess;
    float urgentStep; //緊急訊息回復減少量
    float failStep; //回復失敗減少量

    //隨機產生緊急訊息
    int maxUrgent = 8;
    int minUrgent = 1;
    int randomUrgent;
    //緊急訊息編號
    int urgentTop = 3;
  
    //每2秒產生
    float randomTimeMax = 2;
    float randomTimeMin = 0;

    bool urgentHappen = false;

    void Start()
    {
        UduinoManager.Instance.pinMode(phonePin, PinMode.Input);

        int initialTouch = UduinoManager.Instance.digitalRead(phonePin);
        touchDetected = (initialTouch == 1);

        urgentMess.SetActive(false);

        //常態減少
        normalStep = replyLiner.fillAmount / lessTime;
        StartCoroutine(NormalLess());

        //緊急訊息回復失敗減少
        failStep = replyLiner.fillAmount / 15;

        //一般增加
        normalPlusStep = replyLiner.fillAmount / 60;

        //緊急訊息回復Step
        urgentStep = urgentLiner.fillAmount / 15;

        //訊息最大限時
        timeRemaining = maxTime;

    }

    
    void Update()
    {

        NormalReply();


       if(urgentHappen == false)
        {
            RandomMessage();
        }
        
  
        if(randomUrgent == urgentTop )
        {                   
            urgentHappen = true;
            Debug.Log("10秒緊急訊息!!");         
            urgentMess.SetActive(true);
            UrgentReply();
            
            if(timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                replyTimer.fillAmount = timeRemaining / maxTime;
            }
            
        }

         if(randomUrgent == urgentTop && !soundPlayed)
        {
            messageSound.Play();
            soundPlayed = true;
        }
        
        if(urgentLiner.fillAmount == 0)
        {
            Debug.Log("成功回復緊急訊息~");

            ResetUrgent();
        }else if(replyTimer.fillAmount == 0)
        {
            Debug.Log("回復緊急訊息失敗!!");

            replyLiner.fillAmount -= failStep;
            ResetUrgent();

        }
        
        if(replyLiner.fillAmount < 0.5)
        {
            herAvatar.sprite = sadFace;
        }
        else
        {
            herAvatar.sprite = happyFace;

        }


    }

    //隨機產生訊息
    void RandomMessage()
    {
        randomTimeMin += Time.deltaTime;

        if (randomTimeMin >= randomTimeMax)
        {
            randomUrgent = Random.Range(minUrgent,maxUrgent);
            Debug.Log("現在是訊息" + randomUrgent);

            randomTimeMin = 0;
        }


    }

    //一般回覆
    void NormalReply()
    {
        if (CamCtrl.lookDown == true && urgentHappen == false)
        {
            int touch = UduinoManager.Instance.digitalRead(phonePin);

            if (touch == 1 && !touchDetected/*Input.GetKeyUp(KeyCode.Space)*/)
            {
                replyLiner.fillAmount += normalPlusStep;
                touchDetected = true;
                phoneScreen.SetBool("replying", true);
            }
            else if (touch == 0)
            {
                touchDetected = false;
                phoneScreen.SetBool("replying", false);
            }
        }
    }

    //回復緊急訊息
    void UrgentReply()
    {
        if (CamCtrl.lookDown == true)
        {
            int touch = UduinoManager.Instance.digitalRead(phonePin);

            if (touch == 1 && !touchDetected)
            {
                urgentLiner.fillAmount -= urgentStep;
                touchDetected = true;
                phoneScreen.SetBool("replying", true);
            }
            else if (touch == 0)
            {
                touchDetected = false;
                phoneScreen.SetBool("replying", false);
            }
        }
    }

    //重置緊急訊息
    void ResetUrgent()
    {
        soundPlayed = false;
        urgentHappen = false;
        urgentMess.SetActive(false);
        urgentLiner.fillAmount = 1;
        replyTimer.fillAmount = 1;
        randomUrgent = 1;
        timeRemaining = maxTime;
    }


    //常態減少進度條
    IEnumerator NormalLess()
    {
        while(replyLiner.fillAmount > 0)
        {
            yield return new WaitForSeconds(0.5f);

            replyLiner.fillAmount -= normalStep;

            if(replyLiner.fillAmount < 0)
            {
                replyLiner.fillAmount = 0;
            }

        }
    }

   
}
