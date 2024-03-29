﻿// TIE-21106 Software Engineering Methodology, 2015
// Roni Jaakkola, Marko Tuominen, Jaakko Husso, Noora Männikkö, 2015

using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {

    [Range(0, 15)]
    public float moveForce;         // The amount of force added to the object
	public float maxSpeed;

    Rigidbody2D rbody;              // Reference to the rigidbody component
    public bool canChangeGravity = true;   // Whether the player is able to change gravity

	public float defPlayerHealth;

	public LevelManager Lmanager;

	private float playerHealth;
	private static bool hit;

	public bool gravity;
	private static double gravityCooldown= 0.0;

	public static int playerDeaths = 0;

	public static int playerScore = 0;

	// Powerups
	private static double timeGravity = 0.0;
	private static bool constantGravityChange = false;
	private static double timeSlowMotion = 0.0;
	private static bool slowMotion = false;
	private static double timeInvulnerability = 0.0;
	private static bool invulnerability = false;

	// Weapons
	private static Weapon weaponInUse = null;
	private static Weapon laser = null;
	private static int lasershots = 0;
	private static Weapon zigzag = null;
	private static int zigzagshots = 0;

	//Sound effects
	enum Sounds {
		FLOORHIT1 = 0,
		FLOORHIT2 = 1,
		FLOORHIT3 = 2,
		COIN = 3,
		DAMAGE = 4,
		LASER = 5,
		POWERUP = 6,
		CHECKPOINT = 7,
		LEVELEND = 8
	}

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
		min.y = min.y + 9;

		max = Bounds.bounds.max;
		max.y = max.y - 9;

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
				weaponInUse = null;
				laser = null;
				lasershots = 0;
				zigzag = null;
				zigzagshots = 0;
			}

			hit = false;
		}

		//Cooldown of gravity change is reduced every update, if below 0 change is available.
		gravityCooldown -= Time.deltaTime;

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
        if (gravityCooldown <= 0 && (canChangeGravity || constantGravityChange))
        {
            rbody.gravityScale *= -1;
            canChangeGravity = false;
			gravity = (!gravity);
			gravityCooldown = 0.2;
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
		if (GameObject.Find ("GameManager").GetComponent<GameManager>().debugMode 
		    && Input.GetKeyDown(KeyCode.L))
        {
            GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            gameManager.LoadNextLevel();
        }

		// FOR DEBUGGING
		if (GameObject.Find ("GameManager").GetComponent<GameManager>().debugMode 
		    && Input.GetKeyDown(KeyCode.Backspace))
		{
			// Moves player to next checkpoint
			transform.position = Lmanager.NextCheckpoint();
			rbody.velocity = rbody.velocity.normalized * 0;
		}

		// Using weapons
		if (Input.GetKeyDown (KeyCode.Space)) {
			Debug.Log ("Use Weapon");
			Vector3 direction = (rbody.angularVelocity <= 0) ? Vector3.right : Vector3.left;
			if (!gravity)
				direction *= -1;
			if (weaponInUse != null && weaponInUse == laser)
			{
				//weaponInUse.useWeapon();
				//lasershots = weaponInUse.shotsLeft();
				if (lasershots > 0 && laser != null && !laser.isShooting()) {
					--lasershots;
					SoundManager.instance.PlaySingle((int)Sounds.LASER);
					weaponInUse.useWeapon(transform.position, direction); // Shoot
				}
				if (lasershots == 0) {
					laser = null;
					weaponInUse = zigzag; // Change when there are more weapons
				}
			}
			if (weaponInUse != null && weaponInUse == zigzag)
			{
				if (zigzagshots > 0 && zigzag != null && !zigzag.isShooting()){
					--zigzagshots;
					SoundManager.instance.PlaySingle((int)Sounds.LASER);
					weaponInUse.useWeapon(transform.position, direction); // Shoot
				}
				if (zigzagshots == 0) {
					zigzag = null;
					weaponInUse = laser;
				}
			}
		}
		// Change weapon
		if (Input.GetKeyDown (KeyCode.RightShift)) {
			if (weaponInUse == laser && zigzag != null)
				weaponInUse = zigzag;
			else if (weaponInUse == zigzag && laser != null)
				weaponInUse = laser;
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
            //Debug.Log("Player hit a platform!");
            canChangeGravity = true;
			SoundManager.instance.PlaySingle((int)Sounds.FLOORHIT3);
        }

		// Damage player
		if (collider.gameObject.tag == "Damage")
		{
			SoundManager.instance.PlaySingle((int)Sounds.DAMAGE);
			Debug.Log("Player took damage");
			hit = true;
			canChangeGravity = true;
		}

		// Damage player
		if (collider.gameObject.tag == "Death")
		{
			SoundManager.instance.PlaySingle((int)Sounds.DAMAGE);
			Debug.Log("Player took damage");
			hit = true;
			invulnerability = false;
		}

		// Player reaches the end of the level. Change level.
		if (collider.gameObject.tag == "LevelEnd") {
			Debug.Log ("Level end point");
			//GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
			//gameManager.LoadNextLevel();
			SoundManager.instance.PlaySingle((int)Sounds.LEVELEND);
			LevelEndScript endScreen = GameObject.Find("EndScreen").GetComponent<LevelEndScript>();
			endScreen.openEndScreen();
		}

		if (collider.gameObject.tag == "GravityLock")
		{
			SoundManager.instance.PlaySingle((int)Sounds.FLOORHIT2);
			Debug.Log("Gravity locked");
			canChangeGravity = false;
		}

    }
	public static void collectPowerup(string type, float duration, Color color)
	{
		Debug.Log (color);
		if (type == "gravity") {
			SoundManager.instance.PlaySingle((int)Sounds.POWERUP);
			Debug.Log ("Gravity unlocked by powerup");
			constantGravityChange = true;
			timeGravity = duration;
			srenderer.color = color;
		} else if (type == "time") {
			SoundManager.instance.PlaySingle((int)Sounds.POWERUP);
			Debug.Log ("Slow motion by powerup");
			Time.timeScale = 0.5f;
			slowMotion = true;
			timeSlowMotion = duration;
			srenderer.color = color;
		} else if (type == "invulnerability") {
			SoundManager.instance.PlaySingle((int)Sounds.POWERUP);
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
			SoundManager.instance.PlaySingle((int)Sounds.DAMAGE);
			return false;
		}
	}

	public static bool gravityChangeEnabled()
	{
		return constantGravityChange;
	}

	public static void collectWeapon(string type, Weapon weapon)
	{
		if (type == "laser") {
			if (laser == null)
				laser = weapon;
			lasershots += laser.shotsLeft();
			if (weaponInUse == null)
				weaponInUse = laser;
		}
		if (type == "zigzag") {
			if (zigzag == null)
				zigzag = weapon;
			zigzagshots += zigzag.shotsLeft();
			if (weaponInUse == null)
				weaponInUse = zigzag;
		}
	}

	public static string currentWeapon()
	{
		if (weaponInUse != null && weaponInUse == laser)
			return "Laser";
		if (weaponInUse != null && weaponInUse == zigzag)
			return "Zigzag";
		return null;
	}

	public static int shotsLeft()
	{
		if (weaponInUse != null && weaponInUse == laser)
			return lasershots;
		if (weaponInUse != null && weaponInUse == zigzag)
			return zigzagshots;
		return 0;
	}

}
