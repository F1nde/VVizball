using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    [Range(0, 15)]
    public float moveForce;         // The amount of force added to the object

    Rigidbody2D rbody;              // Reference to the rigidbody component
    bool canChangeGravity = true;   // Whether the player is able to change gravity

	public float defPlayerHealth;

	public Transform newCheckpoint;

	private Vector3 checkpoint;
	private float playerHealth;
	private bool hit;

	public static int playerDeaths = 0;

	// Use this for initialization
	void Start () 
    {
        // Get reference to the rigidbody component in game object
        rbody = GetComponent<Rigidbody2D>();
		playerHealth = defPlayerHealth;
		checkpoint = new Vector3(0,0,0);
	}
	
	// FixedUpdate is fixed to the framerate
	void FixedUpdate () 
    {
		Movement();

		if(hit)
		{
			--playerHealth;

			if(playerHealth == 0)
			{
				transform.position = checkpoint;
				playerHealth = defPlayerHealth;
				canChangeGravity = true;
				++playerDeaths;
			}

			hit = false;
		}
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

		// Damage player
		if (collider.gameObject.tag == "Damage")
		{
			Debug.Log("Player took damage");
			hit = true;
			canChangeGravity = true;
		}

		// 
		if (collider.gameObject.tag == "Checkpoint")
		{
			Debug.Log("Checkpoint!");
			newCheckpoint = GetComponent<Transform>();
			checkpoint = new Vector3(newCheckpoint.position.x, newCheckpoint.position.y + 3, newCheckpoint.position.z);
			canChangeGravity = true;
		}
    }

}
