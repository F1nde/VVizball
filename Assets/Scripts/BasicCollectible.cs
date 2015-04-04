// TIE-21106 Software Engineering Methodology, 2015
// Noora Männikkö, 2015

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

	public override void collect()
	{
		Debug.Log ("class collect");
		base.collect ();
		PlayerController.playerScore += scoreValue;
	}
	
}

