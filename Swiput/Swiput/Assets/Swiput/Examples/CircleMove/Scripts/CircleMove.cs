using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwiputAPI;

public class CircleMove : MonoBehaviour 
{
	public float MoveSpeed = 100f;

	public Rigidbody2D Rb;

	void FixedUpdate()
	{
		float x = Swiput.HorizontalAxis ();
		float y = Swiput.VerticalAxis ();

		Vector2 moveVec = new Vector2 (x, y);
		moveVec = moveVec * MoveSpeed * Time.deltaTime;

		Rb.AddForce(moveVec, ForceMode2D.Impulse);
	}
}
