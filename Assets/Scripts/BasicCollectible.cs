// TIE-21106 Software Engineering Methodology, 2015
// Roni Jaakkola, Marko Tuominen, Jaakko Husso, Noora Männikkö, 2015

using UnityEngine;
using System.Collections;

public class BasicCollectible : Powerup
{
	private int scoreValue; // Score gained when collected

	// Use this for initialization
	void Start ()
	{
		scoreValue = 1; // Decide a proper value
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	// When collected
	public override void collect()
	{
		base.collect ();
		PlayerController.playerScore += scoreValue;
	}
	
}

