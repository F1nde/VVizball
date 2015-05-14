// TIE-21106 Software Engineering Methodology, 2015
// Roni Jaakkola, Jaakko Husso, Marko Tuominen, Noora Männikkö, 2015

using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	public bool pushed;

	//Sound effects
	enum Sounds {
		FLOORHIT1 = 0,
		FLOORHIT2 = 1,
		FLOORHIT3 = 2,
		COIN = 3,
		DAMAGE = 4,
		LASER = 5,
		POWERUP = 6,
		CHECKPOINT = 7,
		LEVELEND = 8
	}

	public SpriteRenderer spriteRenderer;

	void OnTriggerEnter2D(Collider2D other)
	{
		SoundManager.instance.PlaySingle((int)Sounds.CHECKPOINT);
		Debug.Log("Player pushed button!");
		pushed = true;
		spriteRenderer.color = Color.cyan;
	}

	public bool State()
	{
		return pushed;
	}
}

