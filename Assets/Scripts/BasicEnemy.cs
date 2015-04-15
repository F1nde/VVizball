using UnityEngine;
using System.Collections;

public class BasicEnemy : MonoBehaviour
{
	bool state;

	// Use this for initialization
	void Start ()
	{
		state = true;
	}

	// Remove when dead
	public void killEnemy()
	{
		renderer.enabled = false;
		collider2D.enabled = false;
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log ("Player hit an enemy!");
		if (PlayerController.hitEnemy ()) {
			killEnemy ();
		}
		state = false;
	}
	
	public void Reset()
	{
		if (!state)
		{
			renderer.enabled = true;
			collider2D.enabled = true;
			state = true;
		}
	}
}

