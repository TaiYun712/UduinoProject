using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;


public class CamCtrl : MonoBehaviour
{
    UduinoManager u;

    public int camPin = 2;

    public static bool lookDown = false;

    bool backDetected = false;

    public Animator seeDown;
    public Animator myPhone;
    public Animator myMessage;

    void Start()
    {
        UduinoManager.Instance.pinMode(camPin, PinMode.Input);

        int initialCam = UduinoManager.Instance.digitalRead(camPin);
        backDetected = (initialCam == 1);

    }


    void Update()
    {
        int cam = UduinoManager.Instance.digitalRead(camPin);
       // Debug.Log("cam is" + cam);

        if (cam == 0 || Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = new Vector3 (0, -5, -10);
            lookDown = true;

            seeDown.SetBool("lookdown", true);
            myPhone.SetBool("lookphone", true);
            myMessage.SetBool("chackmes", true);
        }
        else if (cam == 1 || Input.GetKeyUp(KeyCode.DownArrow)) 
        {
            transform.position = new Vector3(0, 0, -10);
            lookDown = false;

            seeDown.SetBool("lookdown", false);
            myPhone.SetBool("lookphone", false);
            myMessage.SetBool("chackmes", false);

        }

       
    }
}
