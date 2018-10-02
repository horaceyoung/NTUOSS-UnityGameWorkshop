using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 velocity;             // Store the velocity of Player horizontally and vertically
    private float horizontalVelocityLimit; // Set a limit to Player's maximum horizontal velocity
    private bool walk, walkLeft, walkRight, jump; // State Boolean variables to record the state of the player

    public enum PlayerState // Enum is a set of state variables
    {
        idle,
        walking,
        jumping
    }

    public PlayerState playerState; // Player state to keep track of

    private Rigidbody2D playerRb;  //Store a reference to the Rigidbody2D component required to use 2D Physics.

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        playerRb = GetComponent<Rigidbody2D>();
        playerState = PlayerState.idle;
    }

    void Update()
    {
        UpdatePlayerState(); // Update player State
        UpdatePlayerAnimation(); // Because Animation is not related to Physics, update under Update()
        UpdateHitQuestionBlock(transform.position);
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        ApplyForceToPlayer(); // Call the ApplyForceToPlayer() here
    }

    void ApplyForceToPlayer()
    {

        Vector3 scale = transform.localScale; // Reference Player's scale

        CheckPlayerInput(); // Check the Player's input here

        if (walk)
        {
            Debug.Log("The play wants to walk"); // Log is useful for debugging

            if (walkLeft)
            {
                playerRb.AddForce(Vector2.left * velocity.x); // Apply the force to the player
                scale.x = -1; // Change the direction the player is facing
            }
            else if (walkRight)
            {
                playerRb.AddForce(Vector2.right * velocity.x);
                scale.x = 1;
            }

        }

        if (jump)
        {
            Debug.Log("The player wants to jump");
            AudioSource audioJump = GetComponent<AudioSource>(); // reference to the audiosource;
            audioJump.Play(); // Play the audio clip.
            Vector2 tempVelocity = playerRb.velocity; // Store the current velocity for we want the current horizontal velocity not to change
            playerRb.velocity = new Vector2(tempVelocity.x, velocity.y); // Give the rigidbody an initial vertical velocity
        }


        transform.localScale = scale; // Update Player's Tranform.scale Here
    }

    void CheckPlayerInput()
    {
        // Declare three bools to check the state of keyboard input
        bool input_left = Input.GetKey(KeyCode.LeftArrow);
        bool input_right = Input.GetKey(KeyCode.RightArrow);
        bool input_space = Input.GetKeyDown(KeyCode.Space);

        walk = input_left || input_right; // The player is walking when it's either walking left/right

        walkLeft = input_left && !input_right;

        walkRight = !input_left && input_right;

        jump = input_space; // The player will jump is space is pressed
    }

    void UpdatePlayerState()
    {
        if (playerRb.velocity.y != 0) // If the player has a vertical velocity, meaning the player is in the air
        {
            playerState = PlayerState.jumping; // Change the player's state
        }
        else if(playerRb.velocity.x != 0) // If the player has a horizontal velocity but no vertical, meaning the player is walking
        {
            playerState = PlayerState.walking;
        }
        else // Else the player is idle
        {
            playerState = PlayerState.idle;
        }
    }

    void UpdateHitQuestionBlock(Vector3 pos)
    {
        Vector2 rayOriginTop = new Vector2(pos.x, pos.y + 1.2f);

        RaycastHit2D hitTop = Physics2D.Raycast(rayOriginTop, Vector2.up, velocity.y * Time.deltaTime);
        Debug.Log("Function is called");
        if (hitTop.collider != null)
        {
            Debug.Log("Top is hit");
            if (hitTop.collider.tag == "QuestionBlock")
            {
                hitTop.collider.GetComponent<QuestionBlock>().UpdateBounce();
            }

        }
    }

    void UpdatePlayerAnimation()
    {
        if (playerState == PlayerState.idle) // Check the state of the player
        {
            // Set the Boolean state value accordingly
            GetComponent<Animator>().SetBool("isJumping", false); 
            GetComponent<Animator>().SetBool("isRunning", false);

        }

        if (playerState == PlayerState.jumping)
        {
            GetComponent<Animator>().SetBool("isJumping", true);
            GetComponent<Animator>().SetBool("isRunning", false);
        }

        if (playerState == PlayerState.walking)
        {
            GetComponent<Animator>().SetBool("isJumping", false);
            GetComponent<Animator>().SetBool("isRunning", true);
        }

    }

}