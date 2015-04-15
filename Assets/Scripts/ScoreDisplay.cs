// TIE-21106 Software Engineering Methodology, 2015
// Roni Jaakkola, Marko Tuominen, Jaakko Husso, Noora Männikkö, 2015

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
