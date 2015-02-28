using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {

	public GameObject Panel;
	private bool paused = false;

	// Use this for initialization
	void Start () {
		Panel.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape) && !paused) {
			Panel.gameObject.SetActive (true);
			Time.timeScale = 0;
			paused = true;
		} else if (Input.GetKeyDown (KeyCode.Escape) && paused) {
			resume ();
		}
	}

	public void resume() {
		Panel.gameObject.SetActive(false);
		Time.timeScale = 1;
		paused = false;
	}

	public void exit() {
		Application.Quit ();
	}
}
