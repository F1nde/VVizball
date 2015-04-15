// TIE-21106 Software Engineering Methodology, 2015
// Roni Jaakkola, Marko Tuominen, Jaakko Husso, Noora Männikkö, 2015

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeathDisplay : MonoBehaviour {
	
	Text deathCounter;
	private int deaths = 0;

	void Start () {
		deaths = PlayerController.playerDeaths;
		deathCounter = gameObject.GetComponent<Text> ();
		deathCounter.text = "Deaths: " + deaths;
	}

	void Update () {
		//Fetch deaths
		deaths = PlayerController.playerDeaths;
		deathCounter.text = "Deaths: " + deaths;
	}

}
