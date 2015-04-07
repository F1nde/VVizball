// TIE-21106 Software Engineering Methodology, 2015
// Noora Männikkö, 2015

using UnityEngine;
using System.Collections;

public class GravityPowerup : Powerup
{
	private float durationSecs;

	// Use this for initialization
	void Start ()
	{
		durationSecs = 10.0f; // Decide a proper value
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	// When collected
	public override void collect()
	{
		base.collect ();
		PlayerController.collectPowerup ("gravity", durationSecs);
	}
}

