using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

    GameManager gameManager;

    public GameObject highscoresPanel;
    private bool scoresOpen = false;

    public GameObject settingsPanel;
    private bool settingsOpen = false;


	// Use this for initialization
	void Start () 
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
