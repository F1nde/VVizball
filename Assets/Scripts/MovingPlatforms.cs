using UnityEngine;
using System.Collections;

public class MovingPlatforms : MonoBehaviour {

	public Transform startPoint;
	public Transform endPoint;
	public Transform platform;

	public float platformSpeed;
	
	Vector3 direction;
	Transform destination;

	void Start ()
	{
		SetDestination(startPoint);
	}

	void FixedUpdate()
	{
		platform.rigidbody2D.MovePosition(platform.position + direction * platformSpeed * Time.fixedDeltaTime);

		if (Vector3.Distance (platform.position, destination.position) < platformSpeed * Time.fixedDeltaTime)
		{
			SetDestination(destination == startPoint ? endPoint: startPoint);
		}
	}
	
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawLine (startPoint.position, endPoint.position);
	}

	void SetDestination(Transform dest)
	{
		destination = dest;
		direction = (destination.position - platform.position).normalized;
	}
}
