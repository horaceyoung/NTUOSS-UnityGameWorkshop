using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject player; // Reference to the player game object


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        UpdateCameraPosition(); // Invoke Here
    }

    void UpdateCameraPosition()
    {
        float velocity = 0f; // Initialze a velocity for SmoothDamp, this value is not important.
        Vector3 origin = transform.position; // Reference to the camera position;
        Vector3 target = player.transform.position; // Reference to the player position
        if (target.x - origin.x!= 0f) // Whenever the player moves ahead of the camera
        {
            Debug.Log("Camera is moving");
            float x = Mathf.SmoothDamp(origin.x, target.x, ref velocity, 0.001f); 
            // Smooth the transition from the horizontal value of the camera to the horizontal value of the player
            transform.position = new Vector3(x, transform.position.y, transform.position.z); // Update the transform
        }
        
    }
}
