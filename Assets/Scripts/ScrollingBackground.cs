using UnityEngine;
using System.Collections;

public class ScrollingBackground : MonoBehaviour 
{
	public float speed = 0;
	float pos = 0;
	
	public void move(bool direction)
	{
		pos += speed;
		if (pos > 1.0f)
		{
			pos-= 1.0f;
		}

		if (direction == true)
		{
			//renderer.material.mainTextureOffset = new Vector2 ((Time.time * speed) % 2, 0f);
			renderer.material.mainTextureOffset = new Vector2(pos, 1);
		}
		else
		{
			//renderer.material.mainTextureOffset = new Vector2 (-(Time.time * speed) % 2, -0f);
			renderer.material.mainTextureOffset = new Vector2(pos, 1);
		}
	}

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
