using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    [Range(0, 15)]
    public float moveForce;         // The amount of force added to the object

    Rigidbody2D rbody;              // Reference to the rigidbody component
    bool canChangeGravity = true;   // Whether the player is able to change gravity

	// Use this for initialization
	void Start () 
    {
        // Get reference to the rigidbody component in game object
        rbody = GetComponent<Rigidbody2D>();
	}
	
	// FixedUpdate is fixed to the framerate
	void FixedUpdate () 
    {
		Movement();
	}

    void ChangeGravity()
    {
        if (canChangeGravity)
        {
            rbody.gravityScale *= -1;
            canChangeGravity = false;
        }

        Debug.Log("Gravity changed!");
    }
	
	void Movement ()
	{
        // PLAYER MOVEMENT INPUT
        // =====================
		// - WASD and regular keys included
        // - If you want to change these, go to Edit->Project Settings->Input

        if (Input.GetButton("Gravity1") || Input.GetButton("Gravity2")){
			ChangeGravity();
		}

        if (Input.GetButton("Right")) {
            rbody.AddForce(Vector3.right * moveForce);
        }

        if (Input.GetButton("Left")) {
            rbody.AddForce(Vector3.left * moveForce);
        }

	}

    void OnCollisionEnter2D(Collision2D collider)
    {
        // Reset gravity bool if player hits a platform
        if (collider.gameObject.tag == "Platform") {
            Debug.Log("Player hit a platform!");
            canChangeGravity = true;
        }
    }

}
