// TIE-21106 Software Engineering Methodology, 2015
// Roni Jaakkola, Marko Tuominen, Noora Männikkö, 2015

using UnityEngine;
using System.Collections;

public class LaserWeapon : Weapon
{
	private int shots;

	public LaserShot laserBeam;

	// Use this for initialization
	void Start ()
	{
		shots = 5; // define proper count
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	// When collected
	public override void collect()
	{
		base.collect ();
		PlayerController.collectWeapon ("laser", this);
	}

	// Do things when used
	public override void useWeapon(Vector3 startPoint, Vector3 dir)
	{
		base.useWeapon (startPoint, dir);
		if (shots > 0) {
			Debug.Log ("Shooting"); 
			//--shots;
			laserBeam.setPosition(startPoint, dir);
		}
	}

	public override int shotsLeft()
	{
		return shots;
	}
	
	public override bool isShooting(){
		return laserBeam.isShooting ();
	}

}

