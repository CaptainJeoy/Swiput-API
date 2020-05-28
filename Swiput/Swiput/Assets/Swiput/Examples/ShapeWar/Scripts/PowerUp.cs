using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour 
{
	public float Delay = 7f, DownSpeed = 3f;

	public Rigidbody2D Rb;

	public GameManager.PowerUps ThisPowerUp;

	void Start()
	{
		Rb.velocity = Vector2.down * DownSpeed;
	}

	void Update()
	{
		Destroy (this.gameObject, Delay);
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.transform.CompareTag("Player")) 
		{
			Player.Instance.Anim.Play ();

			switch (ThisPowerUp) 
			{
				case GameManager.PowerUps.Multiply:
					GameManager.Instance.IsMultiplier = true;
					break;
				case GameManager.PowerUps.Invincible:
					GameManager.Instance.IsInvincible = true;
					break;
				case GameManager.PowerUps.ExtraHealth:
					GameManager.Instance.IsExtraHealth = true;
					break;
			}

			Destroy (this.gameObject);
		}
	}
}
