using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coil : MonoBehaviour
{
    [SerializeField] private PlayerLogic player;
    [SerializeField] private float temperature;
    [SerializeField] private float stationOverflowStep =15f;
    [SerializeField] private float station1TempStep =20f;
    [SerializeField] private float station2TempStep =10f;
    [SerializeField] private float station3TempStep =15f;
    [SerializeField] private float coolingStep = 5f;


    [SerializeField] private Station station1;
    [SerializeField] private Station station2;
    [SerializeField] private Station station3;

    private bool isConnectedToPlayer =false;
    private bool isConnectedToStation =false;
    private void Awake()
    {
        player = FindObjectOfType<PlayerLogic>();
    }

    private void FixedUpdate()
    {
        if (temperature >= 100) player.playerFaint("Temperature");
        checkConnection();
        changeTemperature();
        //Debug.Log(temperature);
        

    }

    private void checkConnection()
    {
        isConnectedToPlayer = player.UsingCoil;
        if (isConnectedToPlayer)
        {
            isConnectedToStation = player.UsingStation;
        }
        else
        {
            isConnectedToStation = false;
        }
    }
    private void changeTemperature()
    {
        cooling();

        if (isConnectedToStation)
        {
            // STACJA ODLEWNICZA ID 1
            if(player.IdOfConnectedStation == 1)
            {
                if (station1.EnergyPercent > 95)
                {
                    temperature += stationOverflowStep*Time.deltaTime;
                }
                else
                {
                    temperature += station1TempStep * Time.deltaTime;

                }
            }


            // STACJA SPAWALNICZA ID 2
            if (player.IdOfConnectedStation == 2)
            {
                if (station2.EnergyPercent > 95)
                {
                    temperature += stationOverflowStep * Time.deltaTime;
                }
                else
                {
                    temperature += station2TempStep * Time.deltaTime;

                }
            }


            // STACJA PROGRAMISTYCZNA ID 3
            if (player.IdOfConnectedStation == 3)
            {
                if (station3.EnergyPercent > 95)
                {
                    temperature += stationOverflowStep * Time.deltaTime;
                }
                else
                {
                    temperature += station3TempStep * Time.deltaTime;

                }
            }
        }
        if (temperature > 100) temperature = 100;

    }

    private void cooling()
    {
        if (temperature > 80)
        {
            temperature -= 2* coolingStep * Time.deltaTime;
        }
        else if(temperature > 50)
        {

            temperature -= coolingStep * Time.deltaTime;
        }
        else if (temperature > 0)
        {
            temperature -= 0.5f* coolingStep * Time.deltaTime;
        }
        if (temperature < 0)
        {
            temperature = 0;
        }
    }
}
