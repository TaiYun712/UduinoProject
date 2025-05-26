using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossTalk : MonoBehaviour
{
    public AudioSource warnSound;

    public Text bossText; //老闆說的話
    public Animator bossHint; //老闆事件前的警告
    

    //產生隨機事件範圍
    public int maxEvent = 10;
    public int minEvent = 1;
    int randomEvent;

    //隨機事件編號
    int handEvent = 5;
  //  int chackEvent = 3;

    //事件是否發生
    public static bool nowCheer = false;
    public static bool nowChack = false;

   
    //每2秒產生
    float randomTimeMax = 2;
    float randomTimeMin = 0;



    void Start()
    {
      
    }

    
    void Update()
    {
        if (randomEvent == handEvent )
        {
            Debug.Log("現在是舉手事件");

            bossHint.SetTrigger("bosscall");
            warnSound.Play();
            
            Invoke("CheeringEvent", 2f);

            randomEvent = 0;

        }
        else if( !nowCheer)
        {
                RandomEvent();      
        }

        /*  原本重製檢查用
       if(chackscreen.chackEventOver == true)
        {
           // Debug.Log("重製眼睛");
            myEyes.SetActive(false);

            Invoke("CloseSpawnPoint",1f);
        }*/


        if (nowCheer == true)
        {
            bossText.text = "。。。。。。。。。。。。。。拿出你們的幹勁來!!!!!";
        }else if (nowCheer == false)
        {
            bossText.text = "關於這個項目。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。";

        }

    }

    //隨機產生事件
    void RandomEvent() 
    {
        randomTimeMin += Time.deltaTime;

        if(randomTimeMin >= randomTimeMax)
        {
            randomEvent = Random.Range(minEvent, maxEvent);
           // Debug.Log("現在是事件" + randomEvent);

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


    /*  原本觸發檢查用
     else if(randomEvent == chackEvent )
        {
            Debug.Log("現在是檢查事件");

            bossHint.SetTrigger("bosscall");
            chackPointSpawn.SetActive(true);
            myEyes.SetActive(true);

            Invoke("ChackingEvent",2f);

    randomEvent = 0;
        }*/
}
