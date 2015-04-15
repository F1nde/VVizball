// TIE-21106 Software Engineering Methodology, 2015
// Roni Jaakkola, Marko Tuominen, Jaakko Husso, Noora Männikkö, 2015

using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	public bool pushed;

	public SpriteRenderer spriteRenderer;

	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("Player pushed button!");
		pushed = true;
		spriteRenderer.color = Color.cyan;
	}

	public bool State()
	{
		return pushed;
	}
}

