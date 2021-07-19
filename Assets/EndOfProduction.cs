using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfProduction : MonoBehaviour
{
    private int points =0;

    public int Points { get => points; set => points = value; }

    public void AddPoints()
    {
        points += 100;
    }
}
