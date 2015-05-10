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

	public GameObject startPanel;
	private bool startPanelOpen = false;

	public GameObject storyPanel;

	public GameObject nameField;

	public GameObject greeting;

	private bool debugModeOn = false;
	public Toggle debugCheckbox;

	// Use this for initialization
	void Start () 
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        leaderboard = GameObject.Find("GameManager").GetComponent<Leaderboard>();
	}

    public void LoadLevel(int level)
    {
		Debug.Log ("Load level " + level);
		gameManager.debugMode = debugModeOn;
        gameManager.LoadLevel(level);
    }

	public void ToggleStartPanel()
	{
		if (!startPanelOpen) {
			Debug.Log ("Opening Start panel.");
			startPanel.SetActive (true);
			startPanelOpen = true;
		} else {
			Debug.Log ("Closing Start panel.");
			startPanel.SetActive (false);
			startPanelOpen = false;
		}
	}

	public void openStory()
	{
		// Save name
		gameManager.playerName = nameField.GetComponent<Text>().text;
		Debug.Log ("Saved player name " + gameManager.playerName);

		ToggleStartPanel (); // Close start panel

		Debug.Log("Opening Story panel.");
		storyPanel.SetActive(true);
		greeting.GetComponent<Text> ().text = "Hello, " + gameManager.playerName + "!";
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

	public void ToggleDebugMode()
	{
		debugModeOn = debugCheckbox.isOn;
	}

    public void QuitGame()
    {
        Application.Quit();
    }

}
