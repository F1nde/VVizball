using UnityEngine;
using System.Collections;

public class TimePowerup : Powerup
{
	private float durationSecs;

	// Use this for initialization
	void Start ()
	{
		durationSecs = 5.0f; // Decide a proper value
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	// When collected
	public override void collect()
	{
		base.collect ();
		PlayerController.collectPowerup ("time", durationSecs);
	}
}

