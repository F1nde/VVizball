// TIE-21106 Software Engineering Methodology, 2015
// Roni Jaakkola, Marko Tuominen, Jaakko Husso, Noora Männikkö, 2015

using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {

	public LevelManager Lmanager;
	public SpriteRenderer renderer;
	public Sprite[] sprites;
	public Transform playerSpawnPoint;

	void Start() {
		renderer.sprite = sprites[0];//lamp is off
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		renderer.sprite = sprites[1];//Turn lamp on
		Debug.Log("Checkpoint!");
		Lmanager.NewCheckpoint(playerSpawnPoint.position);
	}

	public Vector3 Position()
	{
		return transform.position;
	}
}
