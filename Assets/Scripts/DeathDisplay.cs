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
