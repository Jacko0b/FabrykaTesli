using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    public GameObject Product;
    public GameObject Product2;
    public GameObject Product3;

    [SerializeField] private GameObject spawnPoint;

    [SerializeField] private GameObject lightRed;
    [SerializeField] private GameObject lightYellow;
    [SerializeField] private GameObject lightGreen;

    [SerializeField] private int idOfStation;
    [SerializeField] private PlayerLogic player;
    [SerializeField] private ProductQueue previousQueue;
    [SerializeField] private ProductQueue nextQueue;
    [SerializeField] private Lightbulbs lightbulbs;



    [SerializeField] private float maxEnergy = 200f;
    [SerializeField] private float sweetSpotPercent = 75f;
    [SerializeField] private float overheatPercent = 95f;

    [SerializeField] private float energyStep = 20f;
    [SerializeField] private float requiredEnergyToStart = 100f;
    [SerializeField] private float passiveEnergyDrop = 5f;


    [SerializeField] private float productionCost = 45f;
    [SerializeField] private float productionTime = 3f;
    [SerializeField] private float sweetSpotProductionTime = 1.5f;

    [SerializeField] private float maxRangeFromPlayer = 5f;


    [SerializeField] private float energy;
    [SerializeField] private float energyPercent = 0;

    private bool isConnectedToPlayer;
    private bool isConnectedToCoil;
    private bool itemInProduction = false;


    public int IdOfStation => idOfStation;

    public float Energy { get => energy; set => energy = value; }
    public float EnergyPercent { get => energyPercent; set => energyPercent = value; }

    private void Awake()
    {

        player = FindObjectOfType<PlayerLogic>();
    }
    private void FixedUpdate()
    {
        energyPercent = energy / maxEnergy * 100;
        checkConnection();
        chargeStation();
        changeStationLight();
        passiveDropOfEnergy();
        produceItem();
        //Debug.Log("ID:" + idOfStation + " current energy: " + energy);
    }


    private void checkConnection()
    {
        if (player.UsingStation && Vector3.Distance(player.transform.position, transform.position) < maxRangeFromPlayer)
        {
            isConnectedToPlayer = true;
        }
        else
        {
            isConnectedToPlayer = false;
        }

        if (isConnectedToPlayer)
        {
            isConnectedToCoil = player.UsingCoil;
        }
        else
        {
            isConnectedToCoil = false;
        }
    }
    private void chargeStation()
    {
        if (isConnectedToCoil)
        {
            if (energy < maxEnergy)
            {
                energy += energyStep * Time.deltaTime;
            }
        }
    }

    private void passiveDropOfEnergy()
    {
        if (energy > 0)
        {
            energy -= passiveEnergyDrop * Time.deltaTime;

        }
    }

    private void produceItem()
    {
        if (energy >= requiredEnergyToStart && previousQueue.Currentcapacity > 0 && nextQueue.Currentcapacity < 2)
        {

            StartCoroutine(WaitForProduction());

        }
    }

    IEnumerator WaitForProduction()
    {
        if (!itemInProduction )
        {

            itemInProduction = true;

            //produce item
            if (idOfStation == 1)
            {
                if (energyPercent < overheatPercent && energyPercent > sweetSpotPercent)
                {
                    yield return new WaitForSecondsRealtime(sweetSpotProductionTime);

                }
                else
                {
                    yield return new WaitForSecondsRealtime(productionTime);

                }
                //spawn 1 -> queue1

                var obj = (GameObject)Instantiate(Product, spawnPoint.transform.position, Quaternion.identity);
                obj.GetComponent<Product>().PositionOfDestination = nextQueue.transform;


            }
            else if (idOfStation == 2)
            {
                //queue2 -> station 2 
                //station2 queue2

                var obj = (GameObject)Instantiate(Product, previousQueue.transform.position, Quaternion.identity);
                obj.GetComponent<Product>().PositionOfDestination = spawnPoint.transform;

                if (energyPercent < overheatPercent && energyPercent > sweetSpotPercent)
                {
                    yield return new WaitForSecondsRealtime(sweetSpotProductionTime);

                }
                else
                {
                    yield return new WaitForSecondsRealtime(productionTime);

                }

                obj = (GameObject)Instantiate(Product2, spawnPoint.transform.position, Quaternion.identity);
                obj.GetComponent<Product>().PositionOfDestination = nextQueue.transform;

                previousQueue.Currentcapacity--;


            }
            else if (idOfStation == 3)
            {
                //queue2 station3
                //spawn3 -> wyjazd za mape

                var obj = (GameObject)Instantiate(Product2, previousQueue.transform.position, Quaternion.identity);
                obj.GetComponent<Product>().PositionOfDestination = spawnPoint.transform;

                if (energyPercent < overheatPercent && energyPercent > sweetSpotPercent)
                {
                    yield return new WaitForSecondsRealtime(sweetSpotProductionTime);

                }
                else
                {
                    yield return new WaitForSecondsRealtime(productionTime);
                }
                obj = (GameObject)Instantiate(Product3, spawnPoint.transform.position, Quaternion.identity);
                obj.GetComponent<Product>().PositionOfDestination = nextQueue.transform;

                previousQueue.Currentcapacity--;

                
            }
            energy -= productionCost;
            itemInProduction = false;
        }
        else
        {
            yield return new WaitForEndOfFrame();
        }
    }

    private void changeStationLight()
    {
        if (energyPercent >= overheatPercent)
        {
            //lightup red
            lightRed.SetActive(true);
            lightGreen.SetActive(false);
            lightYellow.SetActive(false);

        }
        else if (energyPercent < overheatPercent && energyPercent > sweetSpotPercent)

        {
            //lightup green
            lightRed.SetActive(false);
            lightGreen.SetActive(true);
            lightYellow.SetActive(false);
        }
        else if( energy > requiredEnergyToStart)
        {
            //lightup yellow
            lightRed.SetActive(false);
            lightGreen.SetActive(false);
            lightYellow.SetActive(true);
        }
        else
        {
            //turnoff
            lightRed.SetActive(false);
            lightGreen.SetActive(false);
            lightYellow.SetActive(false);
        }
    }

    public void addPoints()
    {
        FindObjectOfType<EndOfProduction>().AddPoints();
        lightbulbs.EndLightUp();

    }

}

