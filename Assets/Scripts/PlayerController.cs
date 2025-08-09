using UnityEngine;
public class PlayerController : MonoBehaviour
{

    public bool playerCanMove;
    public float playerSpeed;

    //Boost-related variables
    public bool isBoosting;
    private Vector3 boostDirection;
    private float boostSpeed;
    private float boostTimer;

    private Vector3 inputMovement;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isBoosting)
        {
            Boost();
        }
        else if (playerCanMove)
        {
            Movement();
        }
    }

    void Movement()
    {
        //sets the inputMovement to whatever the player has input
        inputMovement.x = Input.GetAxisRaw("Horizontal");
        inputMovement.y = Input.GetAxisRaw("Vertical");

        //moves the player in the direction the player's input at [playerSpeed] units per second
        inputMovement.Normalize();

        RaycastHit2D rayCheck = Physics2D.Raycast(transform.position, transform.TransformDirection(inputMovement), 0.33f);
        if (rayCheck)
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

    //The alternative to Movement which is called while Boosting. Lasts a limited time.
    void Boost()
    {
        //Loops until boost timer runs out
        transform.Translate(boostDirection * boostSpeed * Time.deltaTime);
        boostTimer -= Time.deltaTime;
        if (boostTimer < 0)
        {
            isBoosting = false;
        }
    }

    /*
    Right now this will boost on any TriggerEnter,
    so if we want to use that for projectiles etc we will need to modify.
    */
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Booster")
        {
            //Default boost settings. Direction is where booster points (local up).
            boostDirection = other.transform.up.normalized;
            boostSpeed = 10f;
            boostTimer = 0.2f;
            isBoosting = true;
        }
    }

}
