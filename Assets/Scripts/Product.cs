using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour
{
    [SerializeField] private Transform positionOfDestination;
    [SerializeField] private float movespeed = 2f;

    public Transform PositionOfDestination { get => positionOfDestination; set => positionOfDestination = value; }

    private void FixedUpdate()
    {
            transform.position = Vector3.MoveTowards(transform.position, positionOfDestination.position, movespeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, positionOfDestination.position) < 0.0001f)
            {
                Destroy(gameObject);
            }

    }
}
