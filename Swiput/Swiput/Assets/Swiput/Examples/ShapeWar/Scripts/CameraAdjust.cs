using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAdjust : MonoBehaviour 
{
	enum AutoZoomOrientation
	{
		HeightBased,
		WidthBased
	}

	public SpriteRenderer AreaReference;
	public Camera cam;

	[SerializeField]
	private AutoZoomOrientation chooseOrientation = AutoZoomOrientation.HeightBased;

	void Awake()
	{
		switch (chooseOrientation) 
		{
			case AutoZoomOrientation.HeightBased:
				AutoHeightZoomAdjust (AreaReference, cam);
				break;
			case AutoZoomOrientation.WidthBased:
				AutoWidthZoomAdjust (AreaReference, cam);
				break;
		}
	}

	void AutoHeightZoomAdjust (SpriteRenderer area, Camera cam)
	{
		cam.orthographicSize = area.bounds.size.y / 2f;
	}

	void AutoWidthZoomAdjust (SpriteRenderer area, Camera cam)
	{
		float widthReso = (area.bounds.size.x * Screen.height) / Screen.width;

		cam.orthographicSize = widthReso / 2f;
	}
}

