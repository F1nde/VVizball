// TIE-21106 Software Engineering Methodology, 2015
// Roni Jaakkola, Marko Tuominen, Jaakko Husso, Noora Männikkö, 2015

using UnityEngine;
using System.Collections;

public class BasicEnemy : MonoBehaviour
{
	bool state;

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

	// Use this for initialization
	void Start ()
	{
		state = true;
	}

	// Remove when dead
	public void killEnemy()
	{
		SoundManager.instance.PlaySingle((int)Sounds.DAMAGE);
		renderer.enabled = false;
		collider2D.enabled = false;
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log ("Collider name: " + other.name);
		if (other.name == "Player"){
			Debug.Log ("Player hit an enemy!");
			if (PlayerController.hitEnemy ()) {
				killEnemy ();
			}
			state = false;
		}
		if (other.name == "Shot"){
			Debug.Log ("Enemy shot!");
			killEnemy();
			Debug.Log (renderer.enabled.ToString() + collider2D.enabled.ToString());
			// renderer still displays sprite??
			state = false;
		}
	}
	
	public void Reset()
	{
		if (!state)
		{
			renderer.enabled = true;
			collider2D.enabled = true;
			state = true;
		}
	}
}

