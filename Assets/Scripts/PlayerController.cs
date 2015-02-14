using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    [Range(0, 15)]
    public float addedForce;        // The amount of force added to the object

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
	}

    void ChangeGravity()
    {
        rbody.gravityScale *= -1;
        Debug.Log("Gravity changed!");
    }

}
