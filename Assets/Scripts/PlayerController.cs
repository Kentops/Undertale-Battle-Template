using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{

    private int i;
    public InputAction moveAction;


    public Transform playerTransform;


    public bool playerCanMove;
    public float playerSpeed;


    private Vector3 inputMovement;


    [SerializeField]
    private InputActionReference Move;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
        playerTransform = GetComponent<Transform>();
        i = 0;


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

        i++;
        if (i > 200) { i = 0;  Debug.Log(""+inputMovement.x); }





        //moves the player in the direction the player's input at [playerSpeed] units per second
        inputMovement.Normalize();
        playerTransform.Translate(inputMovement * playerSpeed * Time.deltaTime);




    }

}
