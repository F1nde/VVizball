// TIE-21106 Software Engineering Methodology, 2015
// Noora Männikkö, 2015

using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
	
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
		DestroyObject (this);
	}
	
}

