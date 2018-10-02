using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    Rigidbody2D EnemyRb; // Reference to enemy's rigidBody
    public float horizontalSpeed; // Enemy's speed
    private AudioSource deathAudio;
    public GameObject background;
    private AudioSource backgroundAudio;
    // Use this for initialization
	void Start () {
        EnemyRb = GetComponent<Rigidbody2D>(); // Initialize the enemy's rigidbody
        deathAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        EnemyRb.velocity = new Vector2(horizontalSpeed, 0);
        PlayerDieDetechtion();
        // Every time a frame updates, set the Enemy's velocity to keep a constant velocity
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
            horizontalSpeed = -horizontalSpeed; // Once the enemy collides with anything, reverse its direction 
    }

    void PlayerDieDetechtion()
    {
        Vector3 pos = transform.position;

        Vector2 originLeft = new Vector2(pos.x - 0.7f, pos.y); // Left ray origin
        Vector2 originRight = new Vector2(pos.x + 0.7f, pos.y); // Right origion

        RaycastHit2D leftHit= Physics2D.Raycast(originLeft, Vector2.left, horizontalSpeed * Time.deltaTime); // 
        RaycastHit2D rightHit = Physics2D.Raycast(originRight, Vector2.right, horizontalSpeed * Time.deltaTime);

        if (leftHit.collider != null || rightHit.collider!= null)
        {
            RaycastHit2D hit;
            if (leftHit.collider != null)
            {
                hit = leftHit;
            }
            else
            {
                hit = rightHit;
            }
            if (hit.collider.tag == "Player")
            {
                Destroy(hit.collider.gameObject);
                deathAudio.Play();

                backgroundAudio = background.GetComponent<AudioSource>();
                backgroundAudio.Stop();
            }

        }
    }
}
