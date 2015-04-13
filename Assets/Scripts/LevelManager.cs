using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	private Vector3 playerSpawnPoint;

	void Start () 
	{
		playerSpawnPoint = new Vector3 (0, 0, 0);
	}
		
	public void newCheckpoint(Vector3 checkPoint)
	{
		playerSpawnPoint = checkPoint;
	}

	public Vector3 currentCheckpoint()
	{
		return playerSpawnPoint;
	}
	
}
