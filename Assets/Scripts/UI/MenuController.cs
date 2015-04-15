// TIE-21106 Software Engineering Methodology, 2015
// Roni Jaakkola, Marko Tuominen, Jaakko Husso, Noora Männikkö, 2015

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    GameManager gameManager;
    Leaderboard leaderboard;

    public GameObject highscoresPanel;
    private bool scoresOpen = false;
    public Text scores;

    public GameObject settingsPanel;
    private bool settingsOpen = false;

	// Use this for initialization
	void Start () 
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        leaderboard = GameObject.Find("GameManager").GetComponent<Leaderboard>();
	}

    public void LoadLevel(int level)
    {
        gameManager.LoadLevel(level);
    }

    public void ToggleHighscores ()
    {
        if (!scoresOpen)
        {
            Debug.Log("Opening Highscores panel.");
            highscoresPanel.SetActive(true);
            scoresOpen = true;
            scores.text = "Loading...";

            StartCoroutine(leaderboard.GetTimes(1, scores));
        }
        else
        {
            Debug.Log("Closing Highscores panel.");
            highscoresPanel.SetActive(false);
            scoresOpen = false;
        }
    }

    public void ToggleSettings ()
    {
        if (!settingsOpen)
        {
            Debug.Log("Opening Settings panel.");
            settingsPanel.SetActive(true);
            settingsOpen = true;
        }
        else
        {
            Debug.Log("Closing Settings panel.");
            settingsPanel.SetActive(false);
            settingsOpen = false;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
