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
    private int levels = 0;
    private int levelShowed = 0;
    public Text levelTitle;
    public Text scores;

    public GameObject settingsPanel;
    private bool settingsOpen = false;

	public GameObject startPanel;
	private bool startPanelOpen = false;

	public GameObject storyPanel;

	public GameObject nameField;

	public GameObject greeting;

    public Slider volumeSlider;

	private bool debugModeOn = false;
	public Toggle debugCheckbox;

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
		MOUSEOVER = 8
	}

	// Use this for initialization
	void Start () 
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        leaderboard = GameObject.Find("GameManager").GetComponent<Leaderboard>();

        levels = gameManager.levels;
        levelShowed = 1;
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

            UpdateScores();
        }
        else
        {
            Debug.Log("Closing Highscores panel.");
            highscoresPanel.SetActive(false);
            scoresOpen = false;
        }
    }

    private void UpdateScores() 
    {
         levelTitle.text = "Level " + levelShowed;
         scores.text = "Loading...";

         StartCoroutine(leaderboard.GetTimes(levelShowed, scores));
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

    public void ChangeVolume()
    {
        gameManager.SetVolume(volumeSlider.value);
    }

	public void ToggleDebugMode()
	{
		debugModeOn = debugCheckbox.isOn;
	}

    public void IncreaseLevel()
    {
        ++levelShowed;
        if (levelShowed > levels)
            levelShowed = 1;
        UpdateScores();
    }

    public void DecreaseLevel()
    {
        --levelShowed;
        if (levelShowed < 1)
            levelShowed = levels;
        UpdateScores();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
