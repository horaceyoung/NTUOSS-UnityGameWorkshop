using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 velocity;             // Store the velocity of Player horizontally and vertically
    private float horizontalVelocityLimit; // Set a limit to Player's maximum horizontal velocity
    private bool walk, walkLeft, walkRight, jump; // State Boolean variables to record the state of the player

    private enum PlayerState // Enum is a set of state variables
    {
        idle,
        walking,
        jumping
    }
    PlayerState playerState; // Player state to keep track of

    private Rigidbody2D playerRb;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        playerRb = GetComponent<Rigidbody2D>();
        playerState = PlayerState.idle;
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

            playerState = PlayerState.walking; // Change the players's state

            if (walkLeft)
            {
                playerRb.AddForce(Vector2.left * velocity.x); // Apply the force to the player
                scale.x = -1;
            }
            else if (walkRight)
            {
                playerRb.AddForce(Vector2.right * velocity.x);
                scale.x = 1;
            }
        }

        transform.localScale = scale; // Update Player's Tranform.scale Here
    }

    void CheckPlayerInput()
    {
        
        bool input_left = Input.GetKey(KeyCode.LeftArrow);
        bool input_right = Input.GetKey(KeyCode.RightArrow);
        bool input_space = Input.GetKeyDown(KeyCode.Space);

        walk = input_left || input_right;

        walkLeft = input_left && !input_right;

        walkRight = !input_left && input_right;

        jump = input_space;
    }
}