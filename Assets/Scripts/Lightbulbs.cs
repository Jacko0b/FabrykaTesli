using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightbulbs : MonoBehaviour
{
    [SerializeField] private ProductQueue queue1;
    [SerializeField] private ProductQueue queue2;
    [SerializeField] private GameObject[] lighbulbs;


    private void FixedUpdate()
    {
        if (queue1.Currentcapacity==0)
        {
            lighbulbs[0].SetActive(false);
            lighbulbs[1].SetActive(false);
        }
        else if (queue1.Currentcapacity == 1)
        {

            lighbulbs[0].SetActive(true);
            lighbulbs[1].SetActive(false);
        }
        else if (queue1.Currentcapacity == 2)
        {

            lighbulbs[0].SetActive(true);
            lighbulbs[1].SetActive(true);
        }
        if (queue2.Currentcapacity == 0)
        {
            lighbulbs[3].SetActive(false);
            lighbulbs[4].SetActive(false);
        }
        else if (queue2.Currentcapacity == 1)
        {

            lighbulbs[3].SetActive(true);
            lighbulbs[4].SetActive(false);
        }
        else if (queue2.Currentcapacity == 2)
        {

            lighbulbs[3].SetActive(true);
            lighbulbs[4].SetActive(true);
        }
    }

    public void EndLightUp()
    {
        StartCoroutine(LightUp());
    }

    IEnumerator LightUp()
    {

        lighbulbs[5].SetActive(true);
        yield return new WaitForSeconds(2);
        lighbulbs[5].SetActive(false);
    }
}
