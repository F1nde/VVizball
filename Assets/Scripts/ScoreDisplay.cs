using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

	Text score;
	private double timer = 0.0;

	// Use this for initialization
	void Start () {
		score = gameObject.GetComponent<Text> ();
		score.text = "Time: " + timer.ToString("0.00") + "s";
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		score.text = "Time: " + timer.ToString("0.00") + "s";
	}
}
