﻿using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	private Vector3 playerSpawnPoint;

	public Powerup[] powerups;

	public CheckPoint[] checkpoints;
	public int next;

	public Transform mapStart;

	void Start () 
	{
		playerSpawnPoint = mapStart.position;
		next = 0;
	}

	// Checkpoints

	public void NewCheckpoint(Vector3 checkPoint)
	{
		if (playerSpawnPoint != checkPoint)
		{
			playerSpawnPoint = checkPoint;
		}
	}

	// Only called when player dies
	public Vector3 CurrentCheckpoint()
	{
		ResetPowerups();
		return playerSpawnPoint;
	}

	// Changes players spawnpoint to next checkpoint
	public Vector3 NextCheckpoint()
	{
		for (var i = 0; i < checkpoints.Length; ++i)
		{
			if(checkpoints[i].Position() == playerSpawnPoint)
			{
				next = i + 1;
			}
		}

		if (next == checkpoints.Length)
		{
			next = 0;
		}

		playerSpawnPoint = checkpoints[next].Position();
		ResetPowerups();
		return playerSpawnPoint;
	}

	// Powerups
	
	void ResetPowerups()
	{
		for (var i = 0; i < powerups.Length; ++i)
		{
			powerups[i].Reset();		
		}
	}
	
}