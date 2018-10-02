using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBlock : MonoBehaviour {
    private float bounceSpeed = 3f; // The speed by which you want the block to bounce
    private float bounceHeight = 0.2f; // The height by which you want the block to bounce
    private bool canBounce = true; // State Checking, a question block can only bounce once
    private Vector3 originalPosition; // The original position of the question block, used to return to original position

	// Use this for initialization
	void Start () {
        originalPosition = transform.position; // Initialize the original position
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    public void UpdateBounce()
    {
        if (canBounce) // Check is the block is eligible to jump
        {
            if(transform.position.y < originalPosition.y + bounceHeight)
            {
                transform.position += Vector3.up * Time.deltaTime * bounceSpeed; // Bounce the block up
            }
            else
            {
                canBounce = false; // When bounce is finished, disable the canBounce
            }
        }
        else
        {
            if (transform.position.y > originalPosition.y)
            {
                transform.position -= Vector3.up * Time.deltaTime * bounceSpeed; // Bouce the block down
            }
        }
    }

}
