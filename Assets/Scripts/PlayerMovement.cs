using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float movementSpeed;
    [SerializeField] private GameObject coilGauntlet;


    private Vector2 movementInput;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    private void FixedUpdate()
    {
        movePlayer();
        
    }
    private void movePlayer()
    {
        checkLowStickTurn();
        rb.AddForce(movementInput * movementSpeed, ForceMode2D.Force);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    } 
    private void checkLowStickTurn()
    {
        if (Mathf.Abs(movementInput.x) < 0.2)
        {
            movementInput.x = 0;
        }
        if (Mathf.Abs(movementInput.y) < 0.2)
        {
            movementInput.y = 0;
        }
    }
    public void OnCoilGauntletUse(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            coilGauntlet.SetActive(true);
        }
        if (context.canceled)
        {
            coilGauntlet.SetActive(false);
        }
    }

    
}
