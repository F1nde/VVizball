// TIE-21106 Software Engineering Methodology, 2015
// Roni Jaakkola, Marko Tuominen, Noora Männikkö, 2015

using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour
{
	private bool state;

	void Start ()
	{
		state = true;
	}

	// Do things when collected
	public virtual void collect()
	{
		renderer.enabled = false;
		collider2D.enabled = false;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log ("Player collected a powerup!");
		collect ();
		state = false;
		//DestroyObject (this);
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

