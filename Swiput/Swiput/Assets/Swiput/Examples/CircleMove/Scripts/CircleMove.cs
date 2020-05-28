using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwiputAPI;

public class CircleMove : MonoBehaviour 
{
	public float MoveSpeed = 100f;

	public Rigidbody2D Rb;

	private float HalfScreenWidth, HalfScreenHeight;

	void Awake()
	{
		HalfScreenWidth = Camera.main.aspect * Camera.main.orthographicSize;

		HalfScreenHeight = Camera.main.orthographicSize;
	}

	void FixedUpdate()
	{
		float x = Swiput.HorizontalAxis ();
		float y = Swiput.VerticalAxis ();

		Vector2 moveVec = new Vector2 (x, y);
		moveVec = moveVec * MoveSpeed * Time.deltaTime;

		Rb.AddForce(moveVec, ForceMode2D.Impulse);

		//Wrap circle around X axis
		if (transform.position.x - transform.lossyScale.x > HalfScreenWidth) 
		{
			transform.position = new Vector3 (-HalfScreenWidth, transform.position.y, transform.position.z);
		}
		if (transform.position.x + transform.lossyScale.x < -HalfScreenWidth) 
		{
			transform.position = new Vector3 (HalfScreenWidth, transform.position.y, transform.position.z);
		}

		//Wrap circle around Y axis
		if (transform.position.y - transform.lossyScale.y > HalfScreenHeight) 
		{
			transform.position = new Vector3 (transform.position.x, -HalfScreenHeight, transform.position.z);
		}
		if (transform.position.y + transform.lossyScale.y < -HalfScreenHeight) 
		{
			transform.position = new Vector3 (transform.position.x, HalfScreenHeight, transform.position.z);
		}
	}
}
