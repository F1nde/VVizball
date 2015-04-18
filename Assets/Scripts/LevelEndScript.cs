// TIE-21106 Software Engineering Methodology, 2015
// Roni Jaakkola, Marko Tuominen, Jaakko Husso, Noora Männikkö, 2015

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelEndScript : MonoBehaviour {

    public Leaderboard leaderboard;
	
	public GameObject Panel;
	public Text Deaths;
	public Text Timer;
	PauseScript pause;

    public Text inputField;
    public Text doneText;
    private bool timeSubmitted = false;
	
	// Use this for initialization
	void Start () {
		Panel.gameObject.SetActive(false);
		pause = GameObject.Find("PauseMenu").GetComponent<PauseScript> ();
        leaderboard = GameObject.Find("GameManager").GetComponent<Leaderboard>();
		Debug.Log (pause == null);
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

    public void submitTime() {
        timeSubmitted = true;
        string name = inputField.text;
        double time = GameObject.Find("Score").GetComponent<ScoreDisplay>().getTimer();

        doneText.text = "Sending..";
        StartCoroutine(leaderboard.PostTime(name, 1, time, doneText));
    }
	
	public void exit() {
		Application.Quit ();
	}
}
