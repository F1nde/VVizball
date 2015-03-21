using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    [Range(0, 15)]
    public float moveForce;         // The amount of force added to the object
	public float maxSpeed;

    Rigidbody2D rbody;              // Reference to the rigidbody component
    bool canChangeGravity = true;   // Whether the player is able to change gravity

	public float defPlayerHealth;

	public Transform newCheckpoint;

	private Vector3 checkpoint;
	private float playerHealth;
	private bool hit;

	public static int playerDeaths = 0;

	public BoxCollider2D Bounds;

	private Vector3 min;
	private Vector3 max;

	// Use this for initialization
	void Start () 
    {
        // Get reference to the rigidbody component in game object
        rbody = GetComponent<Rigidbody2D>();
		playerHealth = defPlayerHealth;
		checkpoint = new Vector3(0,0,0);

		min = Bounds.bounds.min;
		max = Bounds.bounds.max;
	}
	
	// FixedUpdate is fixed to the framerate
	void FixedUpdate () 
    {
		Movement();

		//Debug.Log(rbody.velocity.magnitude);

		// Players current position
		var x = transform.position.x;
		var y = transform.position.y;

		// Check if player leaves camera bounds
		if(x < min.x || x > max.x || y < min.y || y > max.y )
		{
			hit = true;
		}

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

        if (Input.GetButton("Gravity1") || Input.GetButton("Gravity2")) {
			ChangeGravity();
		}

        if (Input.GetButton("Right")) {
            rbody.AddForce(Vector3.right * moveForce);
        }

        if (Input.GetButton("Left")) {
            rbody.AddForce(Vector3.left * moveForce);
        }

        // FOR DEBUGGING
        if (Input.GetKey(KeyCode.L))
        {
            GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            gameManager.LoadNextLevel();
        }

		// Check player's max speed
		if(rbody.velocity.magnitude > maxSpeed)
		{
			rbody.velocity = rbody.velocity.normalized * maxSpeed;
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
		if (collider.gameObject.tag == "CheckpointUp")
		{
			Debug.Log("Checkpoint!");
			newCheckpoint = GetComponent<Transform>();
			checkpoint = new Vector3(newCheckpoint.position.x, newCheckpoint.position.y + 3, newCheckpoint.position.z);
			canChangeGravity = true;
		}

		if (collider.gameObject.tag == "CheckpointDown")
		{
			Debug.Log("Checkpoint!");
			newCheckpoint = GetComponent<Transform>();
			checkpoint = new Vector3(newCheckpoint.position.x, newCheckpoint.position.y - 3, newCheckpoint.position.z);
			canChangeGravity = true;
		}

		// Player reaches the end of the level. Change level.
		if (collider.gameObject.tag == "LevelEnd") {
			Debug.Log ("Level end point");
			GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
			gameManager.LoadNextLevel();
		}

		if (collider.gameObject.tag == "GravityLock")
		{
			Debug.Log("Gravity locked");
			canChangeGravity = false;
		}
    }
}
