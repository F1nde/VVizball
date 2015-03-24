using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelEndScript : MonoBehaviour {
	
	public GameObject Panel;
	public Text Deaths;
	public Text Timer;
	PauseScript pause;
	
	// Use this for initialization
	void Start () {
		Panel.gameObject.SetActive(false);
		pause = GameObject.Find("PauseMenu").GetComponent<PauseScript> ();
		Debug.Log (pause == null);
	}
	
	// Update is called once per frame
	void Update () {
		//nothing to update
	}

	//Opens the endscreen and displays current time and deaths in this level.
	public void openEndScreen() {
		Deaths.text = "Deaths: " + PlayerController.playerDeaths.ToString();
		Timer.text = "Time: " + GameObject.Find("Score").GetComponent<ScoreDisplay>().getTimer().ToString("0.00") + "s";
		Panel.gameObject.SetActive (true);
		Time.timeScale = 0;
		pause.setEnabled (false);
	}

	//Loads the next level and continues time
	public void nextLevel() {
		Panel.gameObject.SetActive(false);
		Time.timeScale = 1;
		GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.LoadNextLevel();
		pause.setEnabled (true);
	}
	
	public void exit() {
		Application.Quit ();
	}
}
