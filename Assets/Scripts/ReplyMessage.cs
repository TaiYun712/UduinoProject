using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;
using UnityEngine.UI;

public class ReplyMessage : MonoBehaviour
{
    UduinoManager u;

    public int phonePin = 3;

    public Image replyLiner; //�^�аT�����i�ױ�
    public Image replyTimer;//�^�_����

    public GameObject myMessages;

    float timeRemaining;
    public float maxTime = 10.0f;

    public Animator phoneScreen;

    bool touchDetected = false; //�O�_����

    float step;//�i�ױ��C����֪����q
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
            //�I���^�_�T��
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

        //�^�_�ɭ�
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            replyTimer.fillAmount = timeRemaining / maxTime;
        }

        if (replyLiner.fillAmount == 0)
        {
            myMessages.SetActive(false);
           // Debug.Log("�^�_���\");
        }
        else if (timeRemaining < 0)
        {
            myMessages.SetActive(false);
           // Debug.Log("�^�_����");

        }


    }
}
