// TIE-21106 Software Engineering Methodology, 2015
// Roni Jaakkola, Marko Tuominen, Jaakko Husso, Noora Männikkö, 2015

using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {

	public GameObject Panel;
	private bool paused = false;
	private bool available = true;
	private float previousTimeScale = 1.0F;

	// Use this for initialization
	void Start () {
		Panel.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape) && !paused && available) {
			Panel.gameObject.SetActive (true);
			previousTimeScale = Time.timeScale;
			Time.timeScale = 0.0F;
			paused = true;
		} else if (Input.GetKeyDown (KeyCode.Escape) && paused && available) {
			resume ();
		}
	}

	public void resume() {
		Panel.gameObject.SetActive(false);
		Time.timeScale = previousTimeScale;
		paused = false;
	}

	public void exit() {
		Application.Quit ();
	}

	public void setEnabled(bool e) {
		available = e;
	}
}
