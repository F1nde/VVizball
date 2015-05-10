// TIE-21106 Software Engineering Methodology, 2015
// Roni Jaakkola, Marko Tuominen, Noora Männikkö, 2015

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponDisplay : MonoBehaviour
{
	Text weapon;
	private int shots;

	// Use this for initialization
	void Start ()
	{
		shots = 0;
		weapon = gameObject.GetComponent<Text> ();
		weapon.text = "";
	}
	
	// Update is called once per frame
	void Update ()
	{
		string type = PlayerController.currentWeapon ();
		if (type != null) {
			shots = PlayerController.shotsLeft ();
			weapon.text = type + ": " + shots;
		} else
			weapon.text = "";
	}
}

