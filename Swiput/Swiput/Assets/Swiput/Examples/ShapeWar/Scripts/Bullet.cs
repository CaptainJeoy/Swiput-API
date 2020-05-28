using UnityEngine;

[System.Serializable]
public struct Properties
{
	public float Delay;
	public int DamagePoint;
	public Rigidbody2D BulletRB;
}

public class Bullet : MonoBehaviour 
{
	public Properties BulletProperties;

	void Update()
	{
		Destroy (this.gameObject, BulletProperties.Delay);
	}
}
