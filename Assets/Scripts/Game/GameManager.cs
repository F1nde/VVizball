using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public int levels = 2;

    private int level = 0; // TODO: Adjust this according to level numbers

	// Use this for initialization
	void Awake () 
    {
        // Make sure that only one instance of this object exists
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        //InitializeGame();
	}

    void InitializeGame()
    {
        // TODO: If some initialization is needed.
    }

    // Load the next level
    // (loads the menu if the there are no more levels!)
    public void LoadNextLevel()
    {
        if ((level + 1) >= levels)
        {
            Debug.Log("Error while loading level: " + (level + 1));
            level = 0;
            Application.LoadLevel(level);
        }
        else
        {
            ++level;
            Application.LoadLevel(level);
        }
    }

    // Load the level specified by the given int
    public void LoadLevel(int level)
    {
        if (level >= levels)
        {
            Debug.Log("Error while loading level: " + level);
            Application.LoadLevel(0);
        }
        else
        {
            Application.LoadLevel(level);
        }
    }
}
