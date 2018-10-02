using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBlock : MonoBehaviour {
    private float bounceSpeed = 3f; // The speed by which you want the block to bounce
    private float bounceHeight = 0.2f; // The height by which you want the block to bounce
    private bool canBounce = true; // State Checking, a question block can only bounce once
    private Vector3 originalPosition; // The original position of the question block, used to return to original position

    public Sprite emptyBlock;

    public GameObject coin; // Refernce to the coin prefab
    private GameObject coinInstance; // The actual coin instantiated
    private Vector3 coinOriginalPosition; 
    private float coinHeight = 2f; // The height that the coins jumps
    private float coinSpeed= 6f; // The speed that the coin jumps

	// Use this for initialization
	void Start () {
        originalPosition = transform.position; // Initialize the original position
        coinInstance = Instantiate(coin, new Vector3(originalPosition.x, originalPosition.y, 10f), Quaternion.identity);
        // Instantiate the coin prefab

    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void UpdateBounce()
    {
        GetComponent<Animator>().enabled = false;// Disable the animation
        GetComponent<SpriteRenderer>().sprite = emptyBlock; // Change the sprite

        CoinBounce();

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

    void CoinBounce()
    {
        Vector3 coinPos = coinInstance.transform.position;
        if (coinPos.y < coinOriginalPosition.y + coinHeight)
        {
            coinPos += Vector3.up * coinSpeed * Time.deltaTime;
        }
        else
        {
            Destroy(coinInstance); // Destroy the coin once its bounced to the designated height
        }
        coinInstance.transform.position = coinPos; // Update position
    }

}
