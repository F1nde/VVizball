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