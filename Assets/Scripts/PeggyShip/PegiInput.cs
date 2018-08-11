using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegiInput : InputController
{
	public bool Shooting;

	void Update () 
	{
		Momentum = new Vector2(0, 0);

		float temp = Input.GetAxis("Horizontal");
		if (temp != 0)
		{
			Momentum += temp * Vector2.right;
		}

		temp = Input.GetAxisRaw("Vertical");
		if (temp != 0)
		{
			Momentum += temp * Vector2.up;
		}

		float action = Input.GetAxis ("Jump");
		if (action != 0) 
		{
			Shooting = true;
		}
	}
}
