// TIE-21106 Software Engineering Methodology, 2015
// Roni Jaakkola, Marko Tuominen, Noora Männikkö, 2015

using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

    public GameObject gameManager;

	// Use this for initialization
	void Awake () 
    {
        // If GameManager does not exist, instantiate it
        if (GameManager.instance == null)
        {
            GameObject newManager = (GameObject) Instantiate(gameManager);
            newManager.name = "GameManager";
        }
	}
}
