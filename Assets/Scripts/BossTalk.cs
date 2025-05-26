using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossTalk : MonoBehaviour
{
    public AudioSource warnSound;

    public Text bossText; //���󻡪���
    public Animator bossHint; //����ƥ�e��ĵ�i
    

    //�����H���ƥ�d��
    public int maxEvent = 10;
    public int minEvent = 1;
    int randomEvent;

    //�H���ƥ�s��
    int handEvent = 5;
  //  int chackEvent = 3;

    //�ƥ�O�_�o��
    public static bool nowCheer = false;
    public static bool nowChack = false;

   
    //�C2����
    float randomTimeMax = 2;
    float randomTimeMin = 0;



    void Start()
    {
      
    }

    
    void Update()
    {
        if (randomEvent == handEvent )
        {
            Debug.Log("�{�b�O�|��ƥ�");

            bossHint.SetTrigger("bosscall");
            warnSound.Play();
            
            Invoke("CheeringEvent", 2f);

            randomEvent = 0;

        }
        else if( !nowCheer)
        {
                RandomEvent();      
        }

        /*  �쥻���s�ˬd��
       if(chackscreen.chackEventOver == true)
        {
           // Debug.Log("���s����");
            myEyes.SetActive(false);

            Invoke("CloseSpawnPoint",1f);
        }*/


        if (nowCheer == true)
        {
            bossText.text = "�C�C�C�C�C�C�C�C�C�C�C�C�C�C���X�A�̪��F�l��!!!!!";
        }else if (nowCheer == false)
        {
            bossText.text = "����o�Ӷ��ءC�C�C�C�C�C�C�C�C�C�C�C�C�C�C�C�C�C�C�C�C�C�C�C�C�C�C�C�C�C�C�C�C�C�C�C�C";

        }

    }

    //�H�����ͨƥ�
    void RandomEvent() 
    {
        randomTimeMin += Time.deltaTime;

        if(randomTimeMin >= randomTimeMax)
        {
            randomEvent = Random.Range(minEvent, maxEvent);
           // Debug.Log("�{�b�O�ƥ�" + randomEvent);

            randomTimeMin = 0;
        }
    }

    void CheeringEvent()
    {     
        nowCheer = true;
    }

    /*
    void ChackingEvent()
    {
        nowChack = true;
    }

    void CloseSpawnPoint()
    {
        chackPointSpawn.SetActive(false);

    }*/


    /*  �쥻Ĳ�o�ˬd��
     else if(randomEvent == chackEvent )
        {
            Debug.Log("�{�b�O�ˬd�ƥ�");

            bossHint.SetTrigger("bosscall");
            chackPointSpawn.SetActive(true);
            myEyes.SetActive(true);

            Invoke("ChackingEvent",2f);

    randomEvent = 0;
        }*/
}
