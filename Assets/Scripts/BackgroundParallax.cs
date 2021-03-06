﻿// TIE-21106 Software Engineering Methodology, 2015
// Roni Jaakkola, Marko Tuominen, Jaakko Husso, Noora Männikkö, 2015

using UnityEngine;
using System.Collections;

public class BackgroundParallax : MonoBehaviour
{
	public Transform Player;
	public Transform[] Backgrounds;

	public float parallexScale;
	public float smoothing;

	private Vector3 lastPosition;

	void Start()
	{
		lastPosition = Player.position;
	}

	void Update()
	{
		var paralax = (lastPosition.x - Player.position.x) * parallexScale;
		
		for(var i = 0; i < Backgrounds.Length; ++i)
		{
			var backgroundTargetPositionX = Backgrounds[i].position.x + paralax;
			Vector3 backgroundTargetPosition = new Vector3(backgroundTargetPositionX, Backgrounds[i].position.y, Backgrounds[i].position.z);
			Backgrounds[i].position = Vector3.Lerp(Backgrounds[i].position, backgroundTargetPosition, smoothing * Time.deltaTime);
		}

		lastPosition = Player.position;
	}
}
