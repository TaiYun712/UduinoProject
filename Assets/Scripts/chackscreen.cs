using System.Collections;
using System.Collections.Generic;
using Uduino;
using UnityEngine;

public class chackscreen : MonoBehaviour
{
   
    float currentX;
    float currentY;

    public float startX = -5;
    public float changeLineX = 2.0f;
    public float chandeLineY = 1.0f;

    public Vector2 startLooking;


    //眼睛
    UduinoManager u;

    public int chackPin = 5;

    bool catchDetected = false;

    bool catchNotice = false;

    bool rememberNo = false;

    int chackCount = 0;

    public static bool chackOver = false; //告訴生成點是否檢查完

    public static bool chackEventOver = false; //

    void Start()
    {
        UduinoManager.Instance.pinMode(chackPin, PinMode.Input);

        chackOver = false;

        chackEventOver = false;
    }

    void Update()
    {
        //眼睛移動
        currentX = transform.position.x;
        currentY = transform.position.y;
    
        if(BossTalk.nowChack == true)
        {
            chackEventOver = false;

            if (chackCount < 2)
            {
                transform.Translate(new Vector2(5, 0) * Time.deltaTime);

                if (transform.position.x > changeLineX)
                {
                    transform.position = new Vector2(startX, currentY - 1);
                }
                else if (currentY < 1)
                {
                    transform.position = new Vector2(-5, 4);
                    chackCount++;
                    Debug.Log("看了" + chackCount + "次");
                    // gameObject.SetActive(false);
                }

            }
            else
            {
                chackOver = true;
            }

        }

        if(chackOver == true)
        {
            BossTalk.nowChack = false;
            chackEventOver = true;
            chackCount = 0;

            Debug.Log("檢查事件結束" +BossTalk.nowChack.ToString());
        }

        //記住重點
        int eyeOn = UduinoManager.Instance.digitalRead(chackPin);

        if(eyeOn == 0  && !catchDetected)
        {
            if(catchNotice == true)
            {
               Debug.Log("記住了");
                rememberNo = true;
                catchDetected = true;
            }
           
            
        }
        else if(eyeOn == 0 && catchNotice == false) 
        {
        
                Debug.Log("記錯了");
              
        }

        if(eyeOn == 1)
        {
            catchDetected = false;
        }
    }


   


    private void OnTriggerEnter2D(Collider2D collision)
    {
      if(collision.tag == "Notice")
        {
          //  Debug.Log("要記住");
            catchNotice = true;
        }

      
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (rememberNo == true && catchNotice == true)
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
      //  Debug.Log("不重要");
        catchNotice = false;
        rememberNo = false;


    }





}
