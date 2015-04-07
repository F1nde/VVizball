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

	public static int playerScore = 0;

	// Powerups
	private static double timeGravity = 0.0;
	private static bool constantGravityChange = false;
	private static double timeSlowMotion = 0.0;
	private static bool slowMotion = false;


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

		timeGravity -= Time.deltaTime;
		if (timeGravity < 0.0) 
		{
			constantGravityChange = false;
		}
		timeSlowMotion -= Time.deltaTime;
		if (timeSlowMotion < 0.0) 
		{
			slowMotion = false;
			Time.timeScale = 1;
		}
	}

    void ChangeGravity()
    {
        if (canChangeGravity || constantGravityChange)
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
			//GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
			//gameManager.LoadNextLevel();
			LevelEndScript endScreen = GameObject.Find("EndScreen").GetComponent<LevelEndScript>();
			endScreen.openEndScreen();
		}

		if (collider.gameObject.tag == "GravityLock")
		{
			Debug.Log("Gravity locked");
			canChangeGravity = false;
		}

    }

	public static void collectPowerup(string type, float duration)
	{
		if (type == "gravity") {
			Debug.Log ("Gravity unlocked by powerup");
			constantGravityChange = true;
			timeGravity = duration;
		} else if (type == "time") {
			Debug.Log ("Slow motion by powerup");
			Time.timeScale = 0.5f;
			slowMotion = true;
			timeSlowMotion = duration;
		}
	}

	public static string getPowerupDuration()
	{
		if (constantGravityChange) 
		{
			Debug.Log (timeGravity.ToString ("0.00") + "s");
			return timeGravity.ToString ("0.00") + "s";
		}
		if (slowMotion) 
		{
			Debug.Log (timeSlowMotion.ToString ("0.00") + "s");
			return timeSlowMotion.ToString ("0.00") + "s";
		}

		return null;
	}
}
