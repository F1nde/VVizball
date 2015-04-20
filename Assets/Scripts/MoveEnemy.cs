// TIE-21106 Software Engineering Methodology, 2015
// Roni Jaakkola, Marko Tuominen, Jaakko Husso, Noora Männikkö, 2015

using UnityEngine;
using System.Collections;

public class MoveEnemy : MonoBehaviour
{
	public Transform startPoint;
	public Transform endPoint;
	public Transform enemy;
	Transform destination;

	float speed;

	Vector3 direction;

	bool state;

	// Use this for initialization
	void Start ()
	{
		state = true;
		speed = 5f;
		setDestination (endPoint);
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		//enemy.rigidbody2D.MovePosition(enemy.position + direction * speed * Time.fixedDeltaTime);

		enemy.Rotate (Vector3.forward * Time.deltaTime * 360);
		enemy.Translate (new Vector3(direction.x * speed * Time.fixedDeltaTime,0,0), Space.World);
		if (Vector3.Distance (enemy.position, destination.position) < speed * Time.fixedDeltaTime)
		{
			setDestination (destination == startPoint ? endPoint : startPoint);
		}

	}

	// Set target location
	void setDestination(Transform dest)
	{
		destination = dest;
		direction = (destination.position - enemy.position).normalized;
	}

	// Change direction
	void changeDirection()
	{
		setDestination (destination == startPoint ? endPoint : startPoint);
	}

	public void Reset()
	{
		if (!state)
		{
			setDestination(startPoint);
			state = true;
		}
	}
}

