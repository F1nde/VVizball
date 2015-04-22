// TIE-21106 Software Engineering Methodology, 2015
// Roni Jaakkola, Marko Tuominen, Jaakko Husso, Noora Männikkö, 2015

using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {

    [Range(0, 15)]
    public float moveForce;         // The amount of force added to the object
	public float maxSpeed;

    Rigidbody2D rbody;              // Reference to the rigidbody component
    bool canChangeGravity = true;   // Whether the player is able to change gravity

	public float defPlayerHealth;

	public LevelManager Lmanager;

	private float playerHealth;
	private static bool hit;

	public bool gravity;

	public static int playerDeaths = 0;

	public static int playerScore = 0;

	// Powerups
	private static double timeGravity = 0.0;
	private static bool constantGravityChange = false;
	private static double timeSlowMotion = 0.0;
	private static bool slowMotion = false;
	private static double timeInvulnerability = 0.0;
	private static bool invulnerability = false;

	private static SpriteRenderer srenderer;


	public BoxCollider2D Bounds;

	private Vector3 min;
	private Vector3 max;

	// Use this for initialization
	void Start () 
    {
        // Get reference to the rigidbody component in game object
        rbody = GetComponent<Rigidbody2D>();
		playerHealth = defPlayerHealth;
		gravity = true;

		min = Bounds.bounds.min;
		max = Bounds.bounds.max;

		srenderer = GetComponent<SpriteRenderer> ();
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
			invulnerability = false;
		}

		if(hit)
		{
			if(invulnerability == false)
			{
				--playerHealth;
			}

			if(playerHealth == 0)
			{
				// Get current checkpoint
				transform.position = Lmanager.CurrentCheckpoint();

				playerHealth = defPlayerHealth;
				canChangeGravity = true;
				++playerDeaths;

				timeGravity = 0.0;
				constantGravityChange = false;
				timeSlowMotion = 0.0;
				slowMotion = false;
				timeInvulnerability = 0.0;
				invulnerability = false;
			}

			hit = false;
		}

		timeGravity -= Time.deltaTime;
		if (timeGravity < 0.0) 
		{
			constantGravityChange = false;
			if (!slowMotion && !invulnerability)
				srenderer.color = new Color(1, 1, 1, 1); // white
		}
		timeSlowMotion -= Time.deltaTime;
		if (timeSlowMotion < 0.0) 
		{
			slowMotion = false;
			Time.timeScale = 1;
			if (!constantGravityChange && !invulnerability)
				srenderer.color = new Color(1, 1, 1, 1); // white
		}
		timeInvulnerability -= Time.deltaTime;
		if (timeInvulnerability < 0.0) 
		{
			invulnerability = false;
			if (!slowMotion && !constantGravityChange)
				srenderer.color = new Color(1, 1, 1, 1); // white
		}
	}

    void ChangeGravity()
    {
        if (canChangeGravity || constantGravityChange)
        {
            rbody.gravityScale *= -1;
            canChangeGravity = false;
			gravity = (!gravity);
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
        if (Input.GetKeyDown(KeyCode.L))
        {
            GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            gameManager.LoadNextLevel();
        }

		// FOR DEBUGGING
		if (Input.GetKeyDown(KeyCode.Backspace))
		{
			// Moves player to next checkpoint
			transform.position = Lmanager.NextCheckpoint();
			rbody.velocity = rbody.velocity.normalized * 0;
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

		// Damage player
		if (collider.gameObject.tag == "Death")
		{
			Debug.Log("Player took damage");
			hit = true;
			invulnerability = false;
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

	public static void collectPowerup(string type, float duration, Color color)
	{
		Debug.Log (color);
		if (type == "gravity") {
			Debug.Log ("Gravity unlocked by powerup");
			constantGravityChange = true;
			timeGravity = duration;
			srenderer.color = color;
		} else if (type == "time") {
			Debug.Log ("Slow motion by powerup");
			Time.timeScale = 0.5f;
			slowMotion = true;
			timeSlowMotion = duration;
			srenderer.color = color;
		} else if (type == "invulnerability") {
			Debug.Log ("Invulnerability by powerup");
			invulnerability = true;
			timeInvulnerability = duration;
			srenderer.color = color;
		}
	}

	public static string getPowerupDuration()
	{
		if (constantGravityChange || slowMotion || invulnerability) 
		{
			return Math.Max (timeGravity, Math.Max(timeSlowMotion, timeInvulnerability)).ToString ("0.00") + "s";
		}
		
		return null;
	}

	public static bool hitEnemy() 
	{
		Debug.Log ("Player hit");
		if (invulnerability) {
			playerScore += 1; // add score
			// kill enemy
			return true;
		} else {
			// Player dies
			hit = true;
			return false;
		}
	}
}
