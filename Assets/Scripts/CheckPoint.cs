using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {

	public LevelManager Lmanager;

	public Transform playerSpawnPoint;
	
	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("Checkpoint!");
		Lmanager.NewCheckpoint(playerSpawnPoint.position);
	}

	public Vector3 Position()
	{
		return transform.position;
	}
}
