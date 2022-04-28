using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D rb;

    //variables that allow us to check if the player is on the ground and is able to jump once
    public Transform groundCheck;
    public LayerMask groundLayer;

    float horizontal;
    [SerializeField] float speed;
    [SerializeField] float jumpingPower;
    [SerializeField] float groundCheckPointRange;
    

    //bool that tells what direction the player is looking in
    bool isFacingRight = true;

    //Variables to identify what player number this is
    PlayerInputManager playerInputManagerRef;
    string playerName;

    int _playerNum;
    public int playerNumber { get { return _playerNum; } }

    int _typeOfAttack;
    public int whatTypeOfAttack { get { return _typeOfAttack; } } // Set what kind of attack the player has inputted: Light Attack=1 Heavy Attack =2

    //Animator Reference that the state scripts can use to control the character animations
    public Animator fighterAnimatiorPub
    {
        get
        {
            return fighterAnimator;
        }

        set
        {
            fighterAnimator= value;
        }
    }
    Animator fighterAnimator;
    public bool isAttacking { get; set; }
    bool isRunning; //if the player inputs any movement, set running animation by activating a bool

    FighterScript fighterScriptRef; //Reference to the fighter script to control damage and effects

    void Start()
    {
        
        fighterAnimator = GetComponent<Animator>();
        playerInputManagerRef = GameObject.Find("Game Manager").GetComponent<PlayerInputManager>();
        //Set the name of the game object to the player number, so the object can be referenced
        _playerNum = playerInputManagerRef.playerCount;
        playerName = "Player" + " " + _playerNum.ToString();
        gameObject.name = playerName;

        gameObject.transform.position = GameObject.Find(playerName + " Spawn Point").transform.position; //Spawn this player to a spawn point based on what number they are
        
        isAttacking = false;

        fighterScriptRef = gameObject.GetComponent<FighterScript>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y); //multiply horizontal input times speed while keeping the vertical momentum
        if (!isFacingRight && horizontal > 0f)
        {
            Flip();

        }
        //Flips the character when they change direction
        if (isFacingRight && horizontal < 0f)
        {
            Flip();

        }

        //Make sure the player is not currently doing a combo before executing a new attack

        
        if(Mathf.Abs(horizontal)<0.01) 
        {
            isRunning = false; //if the absolute value of the player input is 0, set the animation to false
        }
        fighterAnimator.SetBool("isRunning", isRunning); //set the bool in the animator to the value assigned in the script
    }

    //Jumping Mechanics
    private void OnDrawGizmosSelected()
    { //Viusally set the attackPoints range and size
        if (groundCheck == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckPointRange);
    }
    bool isGrounded() //Private method that returns whether the player is on the ground and can jump
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip() //flips the character on the x axis based on input from the player
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;

        localScale.x *= -1f; //Reverse the character's scale on the x axis in order to flip it
        transform.localScale = localScale;

    }

    public void Jump(InputAction.CallbackContext context) //Method to instantiate a jump, but does not read value but instead detects if button was pressed
    {
        if(context.performed && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if(context.canceled && rb.velocity.y > 0) //Checks if the button has been released and if the player is still moving up, multiply vertical velocity by 0.5
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    public void Move(InputAction.CallbackContext context) //this tells us when an action was triggered
    {
        horizontal = context.ReadValue<Vector2>().x; //reads the horizontal value of the player's input
        isRunning = true;
        
    }

    public void LightAttack(InputAction.CallbackContext context)
    {
        if (!isAttacking)
        {
            isAttacking = true;
            
        }
        _typeOfAttack = 1;


    }

    public void HeavyAttack(InputAction.CallbackContext context)
    {
        if (!isAttacking)
        {
            isAttacking = true;
            
        }
        _typeOfAttack = 2;
        
    }


}




//references:
//https://www.youtube.com/watch?v=24-BkpFSZuI
//https://www.youtube.com/watch?v=g_s0y5yFxYg
