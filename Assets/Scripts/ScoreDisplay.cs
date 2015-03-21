using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

	Text score;
	private double timer = 0.0;

	void Start () {
		score = gameObject.GetComponent<Text> ();
		score.text = "Time: " + timer.ToString("0.00") + "s";
	}

	void Update () {
		timer += Time.deltaTime;
		score.text = "Time: " + timer.ToString("0.00") + "s";
	}
}
