// TIE-21106 Software Engineering Methodology, 2015
// Roni Jaakkola, Marko Tuominen, Noora Männikkö, 2015

using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public Button[] Buttons;

	private int buttons_pressed;

	private bool open;

	// State indicates if something needs to be done
	private bool state;
	
	void Start ()
	{
		open = false;
		state = false;
	}

	void FixedUpdate () 
	{
		checkButtonStatus ();

		if (state == true)
		{
			doorControl ();
		}
	}

	void checkButtonStatus()
	{
		for (var i = 0; i < Buttons.Length; ++i)
		{
			if(Buttons[i].State() == true)
			{
				++buttons_pressed;
			}

		}
		if (buttons_pressed == Buttons.Length)
		{
			open = true;
			state = true;
		} 
		else
		{
			buttons_pressed = 0;
		}
	}
	
	public void doorControl()
	{
		if (open)
		{
			renderer.enabled = false;
			collider2D.enabled = false;
		}
		else
		{
			// For closing
			renderer.enabled = true;
			collider2D.enabled = true;
		}
		state = false;
	}
}

