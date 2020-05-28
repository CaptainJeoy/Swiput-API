using UnityEngine;
using SwiputAPI.CrossPlatform;
using System.Collections.Generic;

namespace SwiputAPI
{
	public sealed class Swiput
	{
		private static VirtualSwiput currentSwipe;

		private static float Sensitivity = 3f, Dead = 0.01f, 
		XValue = 0f, YValue = 0f, XValueRectTrans = 0f, 
		YValueRectTrans = 0f, TouchValueRectTrans = 0f, TouchValue = 0f;

		static Swiput()
		{
#if !UNITY_EDITOR
			currentSwipe = new MobileSwiput();
#else
			currentSwipe = new StandaloneSwiput();
#endif
		}
			
		/// <summary>
		/// Generates a random number[int] between Lower and Upper but ignores a set number within the Random Range.
		/// </summary>
		/// <returns>The random number between Lower and Upper without the ignored number.</returns>
		/// <param name="Lower">Lower [inclusive]</param>
		/// <param name="Upper">Upper [exclusive]</param>
		/// <param name="SkipNumber">Ignore number</param>
		public static int GenerateRandomNumberBetweenWithIgnoreNum (int lower, int upper, int ignoreNum)
		{
			List<int> ExtractedList = new List<int> ();

			for (int i = lower; i < upper; i++) 
			{
				ExtractedList.Add (i);
			}

			if (ExtractedList.Contains(ignoreNum)) 
				ExtractedList.Remove (ignoreNum);

			return ExtractedList [Random.Range (0, ExtractedList.Count)];
		}

		/// <summary>
		/// Evaluates the Swipe on Horizontal axis, without
		/// clamping the Horizontality of the swipe angle
		/// </summary>
		/// <returns>The axis</returns>
		public static float HorizontalAxis (bool allowSwipeOverUI = true, string ignoreUILayer = "UI")
		{
			return getHorizontalAxis (allowSwipeOverUI, ignoreUILayer);
		}

		/// <summary>
		/// Evaluates the Swipe on Horizontal axis,
		/// clamps the Horizontality of the swipe angle
		/// </summary>
		/// <returns>The raw axis</returns>
		/// <param name="isSmooth">If set to <c>true</c>, gradually increments from 0 to the raw axis value</param>
		public static float HorizontalAxisRaw (bool isSmooth = false, bool allowSwipeOverUI = true, string ignoreUILayer = "UI")
		{
			return (isSmooth) ? SmoothHorAxis (getHorizontalAxisRaw (allowSwipeOverUI, ignoreUILayer)) : getHorizontalAxisRaw (allowSwipeOverUI, ignoreUILayer);
		}

		/// <summary>
		/// Evaluates the Swipe on Vertical axis, without
		/// clamping the Verticality of the swipe angle
		/// </summary>
		/// <returns>The axis</returns>
		public static float VerticalAxis (bool allowSwipeOverUI = true, string ignoreUILayer = "UI")
		{
			return getVerticalAxis (allowSwipeOverUI, ignoreUILayer);
		}

		/// <summary>
		/// Evaluates the Swipe on Vertical axis,
		/// clamps the Verticality of the swipe angle
		/// </summary>
		/// <returns>The raw axis</returns>
		/// <param name="isSmooth">If set to <c>true</c>, gradually increments from 0 to the raw axis value</param>
		public static float VerticalAxisRaw (bool smooth = false, bool allowSwipeOverUI = true, string ignoreUILayer = "UI")
		{
			return (smooth) ? SmoothVerAxis (getVerticalAxisRaw (allowSwipeOverUI, ignoreUILayer)) : getVerticalAxisRaw (allowSwipeOverUI, ignoreUILayer);
		}
			
		/// <summary>
		/// Evaluates the Swipe on Horizontal axis within the RectTransform area, without
		/// clamping the Horizontality of the swipe angle
		/// </summary>
		/// <returns>The axis within the rect transform</returns>
		/// <param name="screenArea">Rect Transform area for the swipe</param>
		public static float HorizontalAxisInRectTransform (RectTransform screenArea, bool allowSwipeOverUI = true, string ignoreUILayer = "UI")
		{
			screenArea.gameObject.layer = 0;
			return getHorizontalAxisInRectTransform (screenArea, allowSwipeOverUI, ignoreUILayer); 
		}

		/// <summary>
		/// Evaluates the Swipe on Horizontal axis within the RectTransform area,
		/// clamps the Horizontality of the swipe angle
		/// </summary>
		/// <returns>The raw axis within the rect transform.</returns>
		/// <param name="screenArea">Rect Transform area for the swipe</param>
		/// <param name="smooth">If set to <c>true</c>, gradually increments from 0 to the raw axis value</param>
		public static float HorizontalAxisRawInRectTransform (RectTransform screenArea, bool smooth = false, bool allowSwipeOverUI = true, string ignoreUILayer = "UI")
		{
			screenArea.gameObject.layer = 0;
			return (smooth) ? SmoothHorAxisInRectTrans (getHorizontalAxisRawInRectTransform (screenArea, allowSwipeOverUI, ignoreUILayer)) : 
				getHorizontalAxisRawInRectTransform (screenArea, allowSwipeOverUI, ignoreUILayer); 
		}

		 
		/// <summary>
		/// Evaluates the Swipe on the Vertical axis within the RectTransform area, without
		/// clamping the Verticality of the swipe angle
		/// </summary>
		/// <returns>The axis raw in rect transform.</returns>
		/// <param name="screenArea">Rect Transform area for the swipe</param>
		public static float VerticalAxisInRectTransform (RectTransform screenArea, bool allowSwipeOverUI = true, string ignoreUILayer = "UI")
		{
			screenArea.gameObject.layer = 0;
			return getVerticalAxisInRectTransform (screenArea, allowSwipeOverUI, ignoreUILayer); 
		}

		/// <summary>
		/// Evaluates the Swipe on Vertical axis within the RectTransform area,
		/// clamps the Verticality of the swipe angle
		/// </summary>
		/// <returns>The axis within the rect transform.</returns>
		/// <param name="screenArea">Rect Transform area for the swipe</param>
		/// <param name="smooth">If set to <c>true</c>, gradually increments from 0 to the raw axis value</param>
		public static float VerticalAxisRawInRectTransform (RectTransform screenArea, bool smooth = false, bool allowSwipeOverUI = true, string ignoreUILayer = "UI")
		{
			screenArea.gameObject.layer = 0;
			return (smooth) ? SmoothVerAxisInRectTrans (getVerticalAxisRawInRectTransform (screenArea, allowSwipeOverUI, ignoreUILayer)) : 
				getVerticalAxisRawInRectTransform (screenArea, allowSwipeOverUI, ignoreUILayer); 
		}

		/// <summary>
		/// Maps touch to axisVal
		/// </summary> 
		/// <returns>The touch axis value</returns>
		/// <param name="axisVal">Set the axisVal</param>
		/// <param name="smooth">If set to <c>true</c>, gradually increments from 0 to the set axis value</param>
		public static float Touch (float axisVal = 1f, bool smooth = false, bool allowTouchOverUI = true, string ignoreUILayer = "UI")
		{
			bool isTouched = false; 

			return (smooth) ? SmoothTouch (getTouch (axisVal, ref isTouched, allowTouchOverUI, ignoreUILayer)) : 
				getTouch (axisVal, ref isTouched, allowTouchOverUI, ignoreUILayer); 
		}

		/// <summary>
		/// Maps touch to axisVal
		/// </summary>
		/// <returns>The touch axis value</returns>
		/// <param name="isTouched">Check if isTouched</param>
		/// <param name="axisVal">Set the axisVal</param>
		/// <param name="smooth">If set to <c>true</c> gradually increments from 0 to the set axis value</param>
		public static float Touch (ref bool isTouched, float axisVal = 1f, bool smooth = false, bool allowTouchOverUI = true, string ignoreUILayer = "UI")
		{
			return (smooth) ? SmoothTouch (getTouch (axisVal, ref isTouched, allowTouchOverUI, ignoreUILayer)) : 
				getTouch (axisVal, ref isTouched, allowTouchOverUI, ignoreUILayer); 
		}

		/// <summary>
		/// Maps touch within the Rect Transform to axisVal
		/// </summary> 
		/// <returns>The touch axis value</returns>
		/// <param name="screenArea">Rect Transform area for the touch</param>
		/// <param name="axisVal">Set the axisVal</param>
		/// <param name="smooth">If set to <c>true</c>, gradually increments from 0 to the set axis value</param>
		public static float TouchInRectTransform (RectTransform screenArea, float axisVal = 1f, bool smooth = false, bool allowTouchOverUI = true, string ignoreUILayer = "UI")
		{
			screenArea.gameObject.layer = 0;
			bool isTouched = false; 

			return (smooth) ? SmoothTouchInRectTrans (getTouchInRectTransform (screenArea, axisVal, ref isTouched, allowTouchOverUI, ignoreUILayer)) : 
				getTouchInRectTransform (screenArea, axisVal, ref isTouched, allowTouchOverUI, ignoreUILayer); 
		}
			
		/// <summary>
		/// Maps touch within the Rect Transform to axisVal
		/// </summary>
		/// <returns>The touch axis value</returns>
		/// <param name="screenArea">Rect Transform area for the touch</param>
		/// <param name="isTouched">Check if isTouched</param>
		/// <param name="axisVal">Set the axisVal</param>
		/// <param name="smooth">If set to <c>true</c> gradually increments from 0 to the set axis value</param>
		public static float TouchInRectTransform (RectTransform screenArea, ref bool isTouched, float axisVal = 1f, bool smooth = false, bool allowTouchOverUI = true, string ignoreUILayer = "UI")
		{
			screenArea.gameObject.layer = 0;
			return (smooth) ? SmoothTouchInRectTrans (getTouchInRectTransform (screenArea, axisVal, ref isTouched, allowTouchOverUI, ignoreUILayer)) : 
				getTouchInRectTransform (screenArea, axisVal, ref isTouched, allowTouchOverUI, ignoreUILayer); 
		}
			
		//-----------------------------------------------------------------------------//

		private static float getHorizontalAxis (bool allowSwipeOverUI, string ignoreUILayer)
		{
#if !UNITY_EDITOR
			return MobileSwiput.EvaluateHorizontalSwipeStatic(allowSwipeOverUI, ignoreUILayer);
#else
			return StandaloneSwiput.EvaluateHorizontalSwipeStatic(allowSwipeOverUI, ignoreUILayer);
#endif
		}

		private static float getHorizontalAxisRaw (bool allowSwipeOverUI, string ignoreUILayer)
		{
#if !UNITY_EDITOR
			return MobileSwiput.EvaluateHorizontalSwipeRawStatic(allowSwipeOverUI, ignoreUILayer);
#else
			return StandaloneSwiput.EvaluateHorizontalSwipeRawStatic(allowSwipeOverUI, ignoreUILayer);
#endif
		}
			
		private static float getVerticalAxis (bool allowSwipeOverUI, string ignoreUILayer)
		{
#if !UNITY_EDITOR
			return MobileSwiput.EvaluateVerticalSwipeStatic(allowSwipeOverUI, ignoreUILayer);
#else
			return StandaloneSwiput.EvaluateVerticalSwipeStatic(allowSwipeOverUI, ignoreUILayer);
#endif
		}

		private static float getVerticalAxisRaw (bool allowSwipeOverUI, string ignoreUILayer)
		{
#if !UNITY_EDITOR
			return MobileSwiput.EvaluateVerticalSwipeRawStatic(allowSwipeOverUI, ignoreUILayer);
#else
			return StandaloneSwiput.EvaluateVerticalSwipeRawStatic(allowSwipeOverUI, ignoreUILayer);
#endif
		}

		private static float getHorizontalAxisInRectTransform (RectTransform rectTrans, bool allowSwipeOverUI, string ignoreUILayer)
		{
			return currentSwipe.EvaluateHorizontalSwipeInRectTrans (rectTrans, allowSwipeOverUI, ignoreUILayer);
		}

		private static float getHorizontalAxisRawInRectTransform (RectTransform rectTrans, bool allowSwipeOverUI, string ignoreUILayer)
		{
			return currentSwipe.EvaluateHorizontalSwipeRawInRectTrans (rectTrans, allowSwipeOverUI, ignoreUILayer);
		}

		private static float getVerticalAxisInRectTransform (RectTransform rectTrans, bool allowSwipeOverUI, string ignoreUILayer)
		{
			return currentSwipe.EvaluateVerticalSwipeInRectTrans (rectTrans, allowSwipeOverUI, ignoreUILayer);
		}

		private static float getVerticalAxisRawInRectTransform (RectTransform rectTrans, bool allowSwipeOverUI, string ignoreUILayer)
		{
			return currentSwipe.EvaluateVerticalSwipeRawInRectTrans (rectTrans, allowSwipeOverUI, ignoreUILayer);
		}

		private static float getTouch (float axisVal, ref bool isTouched, bool allowTouchOverUI, string ignoreUILayer)
		{
			return currentSwipe.EvaluateTouch (axisVal, ref isTouched, allowTouchOverUI, ignoreUILayer);
		}

		private static float getTouchInRectTransform (RectTransform rectTrans, float axisVal, ref bool isTouched, bool allowTouchOverUI, string ignoreUILayer)
		{
			return currentSwipe.EvaluateTouchInRectTrans (rectTrans, axisVal, ref isTouched, allowTouchOverUI, ignoreUILayer);
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

		private static float SmoothTouchInRectTrans (float RawAxisValue)
		{
			float target = RawAxisValue;

			if ((target > 0 && TouchValueRectTrans < 0) || (target < 0 && TouchValueRectTrans > 0))
				TouchValueRectTrans = 0f;

			TouchValueRectTrans = Mathf.MoveTowards(TouchValueRectTrans, target, Sensitivity * Time.deltaTime);

			return (Mathf.Abs(TouchValueRectTrans) < Dead) ? 0f : TouchValueRectTrans;
		}

		private static float SmoothTouch (float RawAxisValue)
		{
			float target = RawAxisValue;

			if ((target > 0 && TouchValue < 0) || (target < 0 && TouchValue > 0))
				TouchValue = 0f;

			TouchValue = Mathf.MoveTowards(TouchValue, target, Sensitivity * Time.deltaTime);

			return (Mathf.Abs(TouchValue) < Dead) ? 0f : TouchValue;
		}
	}
}