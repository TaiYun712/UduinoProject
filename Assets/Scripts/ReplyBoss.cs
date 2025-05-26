using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;
using UnityEngine.UI;

public class ReplyBoss : MonoBehaviour
{
    public AudioSource handSound;

    UduinoManager u;
    //舉手
    public int handPin = 4;

    int handUoCount = 0;

    bool handDetected = false;

    public Animator myHand;

    public Animator workMans;

    public GameObject cheerCam;

    public Animator seeDown;
    public Animator myPhone;
    public Animator myMessage;

    public Animator lineEffect;
    public Animator bossHand;

    
    void Start()
    {
        UduinoManager.Instance.pinMode(handPin, PinMode.Input);

        int initialHand = UduinoManager.Instance.digitalRead(handPin);
        handDetected = (initialHand == 1);

        
    }

    
    void Update()
    {
        if(BossTalk.nowCheer == true)
        {
            cheerCam.transform.position = new Vector3(0, 0, -10);
            CamCtrl.lookDown = false;

            seeDown.SetBool("lookdown", false);
            myPhone.SetBool("lookphone", false);
            myMessage.SetBool("chackmes", false);

            workMans.SetBool("cheering", true);
            bossHand.SetBool("bosscheer", true);
            lineEffect.SetBool("nowcheer", true);


            if (CamCtrl.lookDown == false)
            {
                //舉手
                int handUp = UduinoManager.Instance.digitalRead(handPin);
                // Debug.Log("hand is" + handUp);

                if (handUp == 0)
                {

                    myHand.SetBool("handup", false);
                    handDetected = false;

                }
                else if (handUp == 1 && !handDetected /*Input.GetKeyDown(KeyCode.W)*/)
                {
                    handSound.Play();
                    myHand.SetBool("handup", true);
                    handUoCount++;
                    Debug.Log("舉手" + handUoCount);
                    handDetected = true;
                }
            }


        }

        if(handUoCount >= 12)
        {
            workMans.SetBool("cheering", false);
            bossHand.SetBool("bosscheer", false);
            lineEffect.SetBool("nowcheer", false);
            Debug.Log("舉完了");

            CheerOver();
        }

    }

  

    void CheerOver()
    {
        BossTalk.nowCheer = false; 
        handUoCount = 0;
        myHand.SetBool("handup", false);
        Debug.Log("舉手事件結束");
    }


}
