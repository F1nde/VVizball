// TIE-21106 Software Engineering Methodology, 2015
// Noora Männikkö, 2015

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CollectionDisplay : MonoBehaviour
{
	Text collection;
	private int score;

	// Use this for initialization
	void Start ()
	{
		score = 0;
		collection = gameObject.GetComponent<Text> ();
		collection.text = "Score: " + score.ToString ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Get score from player
		score = PlayerController.playerScore;
		collection.text = "Score: " + score.ToString ();
		string powerups = PlayerController.getPowerupDuration();
		if (powerups != null) 
		{
			collection.text += "\nPowerup time: " + powerups;
		}

	}
}

