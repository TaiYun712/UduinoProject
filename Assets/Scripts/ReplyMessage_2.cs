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

    //�k���Y��
    public Image herAvatar;
    public Sprite happyFace, sadFace;

    public int phonePin = 3;
    bool touchDetected = false; //�O�_����


    public Image replyLiner; //�^�аT�����i�ױ�
    

    public Animator phoneScreen; //����ù��ʵe
    //�`�A�ݭn���ƭ�

    public float lessTime = 60.0f; //�C���ɪ�
    float normalStep; //�@�뱡�p�U�����
    float normalPlusStep; //�@�몺�^�мW�q

    //���T���ݭn���ƭ�
    float timeRemaining;
    float maxTime = 10.0f;
    
   

    public Image replyTimer;//�^�_�ɭ�
    public Image urgentLiner; //���T�����i�ױ�
    public GameObject urgentMess;
    float urgentStep; //���T���^�_��ֶq
    float failStep; //�^�_���Ѵ�ֶq

    //�H�����ͺ��T��
    int maxUrgent = 8;
    int minUrgent = 1;
    int randomUrgent;
    //���T���s��
    int urgentTop = 3;
  
    //�C2����
    float randomTimeMax = 2;
    float randomTimeMin = 0;

    bool urgentHappen = false;

    void Start()
    {
        UduinoManager.Instance.pinMode(phonePin, PinMode.Input);

        int initialTouch = UduinoManager.Instance.digitalRead(phonePin);
        touchDetected = (initialTouch == 1);

        urgentMess.SetActive(false);

        //�`�A���
        normalStep = replyLiner.fillAmount / lessTime;
        StartCoroutine(NormalLess());

        //���T���^�_���Ѵ��
        failStep = replyLiner.fillAmount / 15;

        //�@��W�[
        normalPlusStep = replyLiner.fillAmount / 60;

        //���T���^�_Step
        urgentStep = urgentLiner.fillAmount / 15;

        //�T���̤j����
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
            Debug.Log("10����T��!!");         
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
            Debug.Log("���\�^�_���T��~");

            ResetUrgent();
        }else if(replyTimer.fillAmount == 0)
        {
            Debug.Log("�^�_���T������!!");

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

    //�H�����ͰT��
    void RandomMessage()
    {
        randomTimeMin += Time.deltaTime;

        if (randomTimeMin >= randomTimeMax)
        {
            randomUrgent = Random.Range(minUrgent,maxUrgent);
            Debug.Log("�{�b�O�T��" + randomUrgent);

            randomTimeMin = 0;
        }


    }

    //�@��^��
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

    //�^�_���T��
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

    //���m���T��
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


    //�`�A��ֶi�ױ�
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
