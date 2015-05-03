// TIE-21106 Software Engineering Methodology, 2015
// Roni Jaakkola, Marko Tuominen, Noora Männikkö, 2015

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GravityDisplay : MonoBehaviour
{
	bool direction;
	bool gravityUnlocked;

	public Sprite[] sprites;
	public SpriteRenderer renderer;

	public PlayerController player;

	public Image[] images;

	// Use this for initialization
	void Start ()
	{
		renderer.sprite = sprites[0];

		direction = player.gravity;
		gravityUnlocked = player.canChangeGravity || PlayerController.gravityChangeEnabled();
	}
	
	// Update is called once per frame
	void Update ()
	{
		bool previousDirection = direction;
		direction = player.gravity;
		gravityUnlocked = player.canChangeGravity || PlayerController.gravityChangeEnabled();
		Debug.Log ("Gravity unlocked " + gravityUnlocked);

		// Turn image upside down when needed
		if (direction != previousDirection) {
			Vector3 scale = transform.localScale;
			scale.y *= -1;
			transform.localScale = scale;
		}

		// Set image
		if (!gravityUnlocked) {
			gameObject.GetComponent<Image>().sprite = sprites[1]; // Gray image
			Debug.Log ("Gray image");
		}
		else
			gameObject.GetComponent<Image>().sprite = sprites[0]; // Normal
	}
}

