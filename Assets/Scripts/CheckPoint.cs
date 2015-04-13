using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {

	public LevelManager Lmanager;

	public Transform playerSpawnPoint;
	
	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("Checkpoint!");
		Lmanager.newCheckpoint(playerSpawnPoint.position);
	}
}
