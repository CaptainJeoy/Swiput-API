using UnityEngine;
using SwiputAPI.CrossPlatform;

namespace SwiputAPI
{
	public sealed class Swiput
	{
		private static VirtualSwiput currentSwipe;

		private static float Sensitivity = 3f, Dead = 0.01f, 
		XValue = 0f, YValue = 0f, XValueRectTrans = 0f, 
		YValueRectTrans = 0f, TapValueRectTrans = 0f;

		static Swiput()
		{
#if !UNITY_EDITOR
			currentSwipe = new MobileSwiput();
#else
			currentSwipe = new StandaloneSwiput();
#endif
		}
			
		/// <summary>
		/// Evaluates the Swipe on Horizontal axis, without
		/// clamping the Horizontality of the swipe angle
		/// </summary>
		/// <returns>The axis</returns>
		public static float HorizontalAxis ()
		{
			return getHorizontalAxis ();
		}

		/// <summary>
		/// Evaluates the Swipe on Horizontal axis,
		/// clamps the Horizontality of the swipe angle
		/// </summary>
		/// <returns>The raw axis</returns>
		/// <param name="isSmooth">If set to <c>true</c>, gradually increments from 0 to the raw axis value</param>
		public static float HorizontalAxisRaw (bool isSmooth = false)
		{
			return (isSmooth) ? SmoothHorAxis (getHorizontalAxisRaw ()) : getHorizontalAxisRaw ();
		}

		/// <summary>
		/// Evaluates the Swipe on Vertical axis, without
		/// clamping the Verticality of the swipe angle
		/// </summary>
		/// <returns>The axis</returns>
		public static float VerticalAxis ()
		{
			return getVerticalAxis ();
		}

		/// <summary>
		/// Evaluates the Swipe on Vertical axis,
		/// clamps the Verticality of the swipe angle
		/// </summary>
		/// <returns>The raw axis</returns>
		/// <param name="isSmooth">If set to <c>true</c>, gradually increments from 0 to the raw axis value</param>
		public static float VerticalAxisRaw (bool smooth = false)
		{
			return (smooth) ? SmoothVerAxis (getVerticalAxisRaw ()) : getVerticalAxisRaw ();
		}
			
		/// <summary>
		/// Evaluates the Swipe on Horizontal axis within the RectTransform area, without
		/// clamping the Horizontality of the swipe angle
		/// </summary>
		/// <returns>The axis within the rect transform</returns>
		/// <param name="screenArea">Rect Transform area for the swipe</param>
		public static float HorizontalAxisInRectTransform (RectTransform screenArea)
		{
			return getHorizontalAxisInRectTransform (screenArea); 
		}

		/// <summary>
		/// Evaluates the Swipe on Horizontal axis within the RectTransform area,
		/// clamps the Horizontality of the swipe angle
		/// </summary>
		/// <returns>The raw axis within the rect transform.</returns>
		/// <param name="screenArea">Rect Transform area for the swipe</param>
		/// <param name="smooth">If set to <c>true</c>, gradually increments from 0 to the raw axis value</param>
		public static float HorizontalAxisRawInRectTransform (RectTransform screenArea, bool smooth = false)
		{
			return (smooth) ? SmoothHorAxisInRectTrans (getHorizontalAxisRawInRectTransform (screenArea)) : getHorizontalAxisRawInRectTransform (screenArea); 
		}

		 
		/// <summary>
		/// Evaluates the Swipe on the Vertical axis within the RectTransform area, without
		/// clamping the Verticality of the swipe angle
		/// </summary>
		/// <returns>The axis raw in rect transform.</returns>
		/// <param name="screenArea">Rect Transform area for the swipe</param>
		public static float VerticalAxisInRectTransform (RectTransform screenArea)
		{
			return getVerticalAxisInRectTransform (screenArea); 
		}

		/// <summary>
		/// Evaluates the Swipe on Vertical axis within the RectTransform area,
		/// clamps the Verticality of the swipe angle
		/// </summary>
		/// <returns>The axis within the rect transform.</returns>
		/// <param name="screenArea">Rect Transform area for the swipe</param>
		/// <param name="smooth">If set to <c>true</c>, gradually increments from 0 to the raw axis value</param>
		public static float VerticalAxisRawInRectTransform (RectTransform screenArea, bool smooth = false)
		{
			return (smooth) ? SmoothVerAxisInRectTrans (getVerticalAxisRawInRectTransform (screenArea)) : getVerticalAxisRawInRectTransform (screenArea); 
		}
			
		/// <summary>
		/// Maps touch within the Rect Transform to axisVal
		/// </summary> 
		/// <returns>The touch axis value</returns>
		/// <param name="screenArea">Rect Transform area for the touch</param>
		/// <param name="axisVal">Set the axisVal</param>
		/// <param name="smooth">If set to <c>true</c>, gradually increments from 0 to the raw axis value</param>
		public static float TouchInRectTransform (RectTransform screenArea, float axisVal = 1f, bool smooth = false)
		{
			bool isTouched = false; 

			return (smooth) ? SmoothTapInRectTrans (getTouchInRectTransform (screenArea, axisVal, ref isTouched)) : 
				getTouchInRectTransform (screenArea, axisVal, ref isTouched); 
		}
			
		/// <summary>
		/// Maps touch within the Rect Transform to axisVal
		/// </summary>
		/// <returns>The touch axis value</returns>
		/// <param name="screenArea">Rect Transform area for the touch</param>
		/// <param name="isTouched">Check if isTouched</param>
		/// <param name="axisVal">Set the axisVal</param>
		/// <param name="smooth">If set to <c>true</c> gradually increments from 0 to the raw axis value</param>
		public static float TouchInRectTransform (RectTransform screenArea, ref bool isTouched, float axisVal = 1f, bool smooth = false)
		{
			return (smooth) ? SmoothTapInRectTrans (getTouchInRectTransform (screenArea, axisVal, ref isTouched)) : 
				getTouchInRectTransform (screenArea, axisVal, ref isTouched); 
		}


		//-----------------------------------------------------------------------------//

		private static float getHorizontalAxis ()
		{
#if !UNITY_EDITOR
			return MobileSwiput.EvaluateHorizontalSwipeStatic();
#else
			return StandaloneSwiput.EvaluateHorizontalSwipeStatic();
#endif
		}

		private static float getHorizontalAxisRaw ()
		{
#if !UNITY_EDITOR
			return MobileSwiput.EvaluateHorizontalSwipeRawStatic();
#else
			return StandaloneSwiput.EvaluateHorizontalSwipeRawStatic();
#endif
		}
			
		private static float getVerticalAxis ()
		{
#if !UNITY_EDITOR
			return MobileSwiput.EvaluateVerticalSwipeStatic();
#else
			return StandaloneSwiput.EvaluateVerticalSwipeStatic();
#endif
		}

		private static float getVerticalAxisRaw ()
		{
#if !UNITY_EDITOR
			return MobileSwiput.EvaluateVerticalSwipeRawStatic();
#else
			return StandaloneSwiput.EvaluateVerticalSwipeRawStatic();
#endif
		}

		private static float getHorizontalAxisInRectTransform (RectTransform rectTrans)
		{
			return currentSwipe.EvaluateHorizontalSwipeInRectTrans (rectTrans);
		}

		private static float getHorizontalAxisRawInRectTransform (RectTransform rectTrans)
		{
			return currentSwipe.EvaluateHorizontalSwipeRawInRectTrans (rectTrans);
		}

		private static float getVerticalAxisInRectTransform (RectTransform rectTrans)
		{
			return currentSwipe.EvaluateVerticalSwipeInRectTrans (rectTrans);
		}

		private static float getVerticalAxisRawInRectTransform (RectTransform rectTrans)
		{
			return currentSwipe.EvaluateVerticalSwipeRawInRectTrans (rectTrans);
		}

		private static float getTouchInRectTransform (RectTransform rectTrans, float axisVal, ref bool isTouched)
		{
			return currentSwipe.EvaluateTouchInRectTrans (rectTrans, axisVal, ref isTouched);
		}
			
		//-----------------------------------------------------------------------------//

		private static float SmoothHorAxis(float RawAxisValue)
		{
			float target = RawAxisValue;

			if ((target > 0 && XValue < 0) || (target < 0 && XValue > 0))
				XValue = 0f;

			XValue = Mathf.MoveTowards(XValue, target, Sensitivity * Time.deltaTime);

			return (Mathf.Abs(XValue) < Dead) ? 0f : XValue;
		}

		private static float SmoothVerAxis(float RawAxisValue)
		{
			float target = RawAxisValue;

			if ((target > 0 && YValue < 0) || (target < 0 && YValue > 0))
				YValue = 0f;

			YValue = Mathf.MoveTowards(YValue, target, Sensitivity * Time.deltaTime);

			return (Mathf.Abs(YValue) < Dead) ? 0f : YValue;
		}

		private static float SmoothHorAxisInRectTrans (float RawAxisValue)
		{
			float target = RawAxisValue;

			if ((target > 0 && XValueRectTrans < 0) || (target < 0 && XValueRectTrans > 0))
				XValueRectTrans = 0f;

			XValueRectTrans = Mathf.MoveTowards(XValueRectTrans, target, Sensitivity * Time.deltaTime);

			return (Mathf.Abs(XValueRectTrans) < Dead) ? 0f : XValueRectTrans;
		}

		private static float SmoothVerAxisInRectTrans (float RawAxisValue)
		{
			float target = RawAxisValue;

			if ((target > 0 && YValueRectTrans < 0) || (target < 0 && YValueRectTrans > 0))
				YValueRectTrans = 0f;

			YValueRectTrans = Mathf.MoveTowards(YValueRectTrans, target, Sensitivity * Time.deltaTime);

			return (Mathf.Abs(YValueRectTrans) < Dead) ? 0f : YValueRectTrans;
		}

		private static float SmoothTapInRectTrans (float RawAxisValue)
		{
			float target = RawAxisValue;

			if ((target > 0 && TapValueRectTrans < 0) || (target < 0 && TapValueRectTrans > 0))
				TapValueRectTrans = 0f;

			TapValueRectTrans = Mathf.MoveTowards(TapValueRectTrans, target, Sensitivity * Time.deltaTime);

			return (Mathf.Abs(TapValueRectTrans) < Dead) ? 0f : TapValueRectTrans;
		}
	}
}