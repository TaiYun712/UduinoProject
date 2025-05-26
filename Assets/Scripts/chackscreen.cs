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


    //����
    UduinoManager u;

    public int chackPin = 5;

    bool catchDetected = false;

    bool catchNotice = false;

    bool rememberNo = false;

    int chackCount = 0;

    public static bool chackOver = false; //�i�D�ͦ��I�O�_�ˬd��

    public static bool chackEventOver = false; //

    void Start()
    {
        UduinoManager.Instance.pinMode(chackPin, PinMode.Input);

        chackOver = false;

        chackEventOver = false;
    }

    void Update()
    {
        //��������
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
                    Debug.Log("�ݤF" + chackCount + "��");
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

            Debug.Log("�ˬd�ƥ󵲧�" +BossTalk.nowChack.ToString());
        }

        //�O���I
        int eyeOn = UduinoManager.Instance.digitalRead(chackPin);

        if(eyeOn == 0  && !catchDetected)
        {
            if(catchNotice == true)
            {
               Debug.Log("�O��F");
                rememberNo = true;
                catchDetected = true;
            }
           
            
        }
        else if(eyeOn == 0 && catchNotice == false) 
        {
        
                Debug.Log("�O���F");
              
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
          //  Debug.Log("�n�O��");
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
      //  Debug.Log("�����n");
        catchNotice = false;
        rememberNo = false;


    }





}
