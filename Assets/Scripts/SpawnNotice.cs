using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNotice : MonoBehaviour
{
    public GameObject noticePart;

    public int[] placeX;
    public int[] placeY;

    bool hasChack = false;

    private void Start()
    {
        StartCoroutine(SpawnPart());
    }

    private void Update()
    {
        if(chackscreen.chackOver == true && !hasChack)
        {
            GameObject[] noticesParts = GameObject.FindGameObjectsWithTag(noticePart.tag);

            if(noticesParts.Length >= 1)
            {
                Debug.Log("抓取失敗");
               
            }
            else
            {
                Debug.Log("全部抓取");
            }

            foreach (GameObject part in noticesParts)
            {
                Destroy(part);
            }

            hasChack = true;

        }
    }

    IEnumerator SpawnPart()
    {
        hasChack = false ;

        int Num = 0;
        while(Num < 3)
        {
            Num += 1;
            int ran_X = Random.Range(0,placeX.Length);
            int ran_Y = Random.Range(0,placeY.Length);

            float ranX = placeX[ran_X];
            float ranY = placeY[ran_Y];


            Vector2 noticePlace = new Vector2(ranX, ranY);

            GameObject clone  = Instantiate(noticePart,noticePlace,transform.rotation);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
