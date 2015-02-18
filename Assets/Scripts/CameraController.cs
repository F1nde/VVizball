using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public Transform Player;

	public Vector2 margin;
	public Vector2 smoothing;

	public BoxCollider2D Bounds;

	private Vector3 min;
	private Vector3 max;

	public bool Follow { get; set; }

	public void Start()
	{
		min = Bounds.bounds.min;
		max = Bounds.bounds.max;
		Follow = true;
	}

	public void Update()
	{
		var x = transform.position.x;
		var y = transform.position.y;

		if(Follow)
		{
			if(Mathf.Abs(x - Player.position.x) > margin.x)
			{
				x = Mathf.Lerp(x, Player.position.x, smoothing.x * Time.deltaTime);
			}

			if(Mathf.Abs(y - Player.position.y) > margin.y)
			{
				y = Mathf.Lerp(y, Player.position.y, smoothing.y * Time.deltaTime);
			}
		}

		var cameraHalfWidth = camera.orthographicSize * ((float)Screen.width / Screen.height);

		x = Mathf.Clamp(x, min.x + cameraHalfWidth, max.x - cameraHalfWidth);
		y = Mathf.Clamp(y, min.y + cameraHalfWidth, max.y - cameraHalfWidth);

		transform.position = new Vector3(x, y, transform.position.z);
	}
}
