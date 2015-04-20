// TIE-21106 Software Engineering Methodology, 2015
// Roni Jaakkola, Marko Tuominen, Jaakko Husso, Noora Männikkö, 2015

using UnityEngine;
using System.Collections;

public class MoveEnemyMultiplePoints : MonoBehaviour
{
	//At least 2 points!
	public Transform[] points;
	public Transform enemy;
	public float speed;

	Transform destination;
	int currentPoint = 0;
	Vector3 direction;

	//bool state;

	// Use this for initialization
	void Start ()
	{
		//state = true;
		currentPoint = 0;
		//Always sets destination to next point
		setDestination ();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		//enemy.rigidbody2D.MovePosition(enemy.position + direction * speed * Time.fixedDeltaTime);

		enemy.Rotate (Vector3.forward * Time.deltaTime * 360);
		enemy.Translate (new Vector3(direction.x * speed * Time.fixedDeltaTime, direction.y * speed * Time.fixedDeltaTime, 0), Space.World);
		if (Vector3.Distance (enemy.position, destination.position) < speed * Time.fixedDeltaTime)
		{
			setDestination ();
		}

	}

	// Set target location
	void setDestination()
	{
		++currentPoint;
		destination = points[currentPoint % points.Length];
		direction = (destination.position - enemy.position).normalized;
	}

	// Change direction - not used??
	void changeDirection()
	{
		setDestination ();
	}

	/*public void Reset()
	{
		if (!state)
		{
			currentPoint = points.Length;
			setDestination();
			state = true;
		}
	}
	*/
}

