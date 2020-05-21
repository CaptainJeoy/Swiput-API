﻿using UnityEngine;

namespace SwiputAPI 
{
	public abstract class VirtualSwiput 
	{
		protected Rect GetScreenCoord(RectTransform uiTrans)
		{
			Vector2 size = Vector2.Scale (uiTrans.rect.size, uiTrans.lossyScale);

			Rect rect = new Rect (uiTrans.position.x, Screen.height - uiTrans.position.y, size.x, size.y);

			rect.x -= (uiTrans.pivot.x * size.x);
			rect.y -= ((1.0f - uiTrans.pivot.y) * size.y);

			return rect;
		}

		public abstract float EvaluateHorizontalSwipeInRectTrans (RectTransform rectTrans);
		public abstract float EvaluateVerticalSwipeInRectTrans (RectTransform rectTrans);

		public abstract float EvaluateHorizontalSwipeRawInRectTrans (RectTransform rectTrans);
		public abstract float EvaluateVerticalSwipeRawInRectTrans (RectTransform rectTrans);

		public abstract float EvaluateTouchInRectTrans (RectTransform rectTrans, float axisVal, ref bool isTouched);
	}
}
