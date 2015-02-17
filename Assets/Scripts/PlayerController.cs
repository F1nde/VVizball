using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    [Range(0, 15)]
    public float moveForce;        // The amount of force added to the object

    Rigidbody2D rbody;              // Reference to the rigidbody component

	// Use this for initialization
	void Start () 
    {
        // Get reference to the rigidbody component in game object
        rbody = GetComponent<Rigidbody2D>();
	}
	
	// FixedUpdate is fixed to the framerate
	void FixedUpdate () 
    {
        if (Input.GetButtonDown("Gravity")) // TODO: Prevent player from changing this constantly
            ChangeGravity();

		Movement();
	}

    void ChangeGravity()
    {
        rbody.gravityScale *= -1;
        Debug.Log("Gravity changed!");
    }
	
	void Movement ()
	{
		// WASD and regular keys included
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
			ChangeGravity();
		}
		
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            rbody.AddForce(Vector3.right * moveForce);
		}

		if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
			ChangeGravity();
		}

		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            rbody.AddForce(Vector3.left * moveForce);
		}
	}

}
