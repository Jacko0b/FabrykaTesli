using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductQueue : MonoBehaviour
{
    [SerializeField] private int currentcapacity;

    public int Currentcapacity { get => currentcapacity; set => currentcapacity = value; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Product"))
        {
            Debug.Log("collision happened");
            currentcapacity++;
            
        }
    }
}
