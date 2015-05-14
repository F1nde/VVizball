// TIE-21106 Software Engineering Methodology, 2015
// Roni Jaakkola, Marko Tuominen, Noora Männikkö, 2015

using UnityEngine;
using System.Collections;

public class BasicCollectible : Powerup
{
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
		SoundManager.instance.PlaySingle((int)Sounds.COIN);
		PlayerController.playerScore += scoreValue;
	}
	
}

