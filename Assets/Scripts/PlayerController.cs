using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public InputAction moveAction;

    public bool playerCanMove;
    public float playerSpeed;

    [SerializeField] private InputActionReference Move;

    private Vector3 inputMovement;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCanMove) Movement();
    }

    void Movement()
    {
        //sets the inputMovement to whatever the player has input
        inputMovement.x = Move.action.ReadValue<Vector2>().x;
        inputMovement.y = Move.action.ReadValue<Vector2>().y;

        //moves the player in the direction the player's input at [playerSpeed] units per second
        inputMovement.Normalize();

        RaycastHit2D rayCheck = Physics2D.Raycast(transform.position, transform.TransformDirection(inputMovement), 0.33f);
        if(rayCheck)
        {
            if (rayCheck.transform.gameObject.layer == LayerMask.NameToLayer("Horizontal Wall"))
            {
                inputMovement.y = 0;
            }
            if (rayCheck.transform.gameObject.layer == LayerMask.NameToLayer("Vertical Wall"))
            {
                inputMovement.x = 0;
            }
            inputMovement.Normalize();
        }


            transform.Translate(inputMovement * playerSpeed * Time.deltaTime);
    }

}
