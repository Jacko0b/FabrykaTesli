using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    [SerializeField] private GameObject coilGauntlet;
    [SerializeField] private GameObject coil;
    [SerializeField] private Menu menu;
    private Rigidbody2D rb;


    [SerializeField] private float usableCoilRange;
    private float distanceFromCoil;
    private bool usingCoil = false;

    private bool stationInRange = false;

    private bool usingStation = false;
    private int idOfConnectedStation = -1;
    private int idOfClosestStation = -1;
    
    public bool UsingStation { get => usingStation; set => usingStation = value; }
    public bool UsingCoil { get => usingCoil; set => usingCoil = value; }
    public int IdOfConnectedStation { get => idOfConnectedStation; set => idOfConnectedStation = value; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }
    void FixedUpdate()
    {
        distanceFromCoil = Vector3.Distance(coil.transform.position, transform.position);
   
        checkUsingCoil();
       // Debug.Log("id of connected station: " + idOfConnectedStation);
      //  Debug.Log("id of closest station: " + idOfClosestStation);

        // Debug.Log("distance from coil: " + distanceFromCoil + " using coil: " + usingCoil             + " using station: " + usingStation);

    }

    private void checkUsingCoil()
    {
        if (coilGauntlet.activeInHierarchy == true && distanceFromCoil < usableCoilRange)
        {
            usingCoil = true;
            //effects of using coil

            if (!usingStation)
            {
                playerFaint("Coil");
            }
        }
        else
        {
            usingCoil = false;
        }
    }
    public void playerFaint(string cause)
    {
        if(cause == "Coil")
        {
            //death from coil
            Debug.Log("fainted from contact with coil");
        }
        if (cause == "Temperature")
        {
            //death from coil
            Debug.Log("fainted from extreme temperature");
        }


    }


    public void OnStationUse(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            useStation();
        }
    }
    private void useStation()
    {
        if (stationInRange)
        {
            if (!usingStation)
            {
                startUsingStation();
            }
            else
            {
                stopUsingStation();
            }
        }

    }
    private void startUsingStation()
    {
        usingStation = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        idOfConnectedStation = idOfClosestStation;

    }
    private void stopUsingStation()
    {
        usingStation = false;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        idOfConnectedStation = -1;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coil")
        {
            playerFaint("Coil");
        }
        if (collision.tag == "Station")
        {
            stationInRange = true;
            idOfClosestStation = collision.GetComponent<Station>().IdOfStation;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Station")
        {
            stationInRange = false;
            stopUsingStation();
            idOfClosestStation = -1;

        }
    }
    
    public void OnMenuButton(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            menu.openMenuIngame();
        }
    }
}
