using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

	Text time;
	private double timer = 0.0;

	void Start () {
		time = gameObject.GetComponent<Text> ();
		time.text = "Time: " + timer.ToString("0.00") + "s";
	}

	void Update () {
		timer += Time.deltaTime;
		time.text = "Time: " + timer.ToString("0.00") + "s";
	}

	public double getTimer(){
		return timer;
	}
}
