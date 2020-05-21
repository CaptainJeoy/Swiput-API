using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwiputAPI;

public class Cubing : MonoBehaviour 
{
	public float rotateSpeed = 7f;

	void FixedUpdate()
	{
		float h = Swiput.HorizontalAxis ();
		float v = Swiput.VerticalAxis ();

		transform.Rotate (Vector3.up, -h * rotateSpeed, Space.World);
		transform.Rotate (Vector3.right, v * rotateSpeed, Space.World);
	}
}
