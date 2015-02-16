using UnityEngine;
using System.Collections;

public class ScrollingBackground : MonoBehaviour 
{
	public float speed = 0;
	float pos = 0;

	void Update()
	{
		pos += speed;
		if (pos > 1.0f)
		{
			pos-= 1.0f;
		}

		renderer.material.mainTextureOffset = new Vector2(pos, 0);
	} 
}
