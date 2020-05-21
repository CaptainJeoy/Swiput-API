using UnityEngine;
using SwiputAPI;

public class Spin : MonoBehaviour 
{
	public RectTransform rightRect;
	public RectTransform leftRect;

	public float spinSpeed = 50f;

	private Rigidbody2D rb;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate()
	{
		float spinAxis = Swiput.VerticalAxisInRectTransform (rightRect);
		float spinAxisOpp = Swiput.VerticalAxisInRectTransform (leftRect);

		rb.AddTorque (spinAxis * spinSpeed);
		rb.AddTorque (-spinAxisOpp * spinSpeed);
	}
}
