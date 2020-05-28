using UnityEngine;

namespace SwiputAPI.CrossPlatform
{
	public class MobileSwiput : VirtualSwiput
	{			
		//Statics
		static Vector3 prevHorPointerPosStatic = Vector3.zero, prevVerPointerPosStatic = Vector3.zero;
		static Vector3 prevHorPointerPosRawStatic = Vector3.zero, prevVerPointerPosRawStatic = Vector3.zero;

		static Vector3 deltaHorPointerPosStatic = Vector3.zero, deltaVerPointerPosStatic = Vector3.zero;
		static Vector3 deltaHorPointerPosRawStatic = Vector3.zero, deltaVerPointerPosRawStatic = Vector3.zero;

		static Vector3 realHorTouchPos = Vector3.zero, realVerTouchPos = Vector3.zero;
		static Vector3 realHorTouchPosRaw = Vector3.zero, realVerTouchPosRaw = Vector3.zero;

		//Swipe In Rect
		Vector3 prevHorPointerRectPos = Vector3.zero, prevVerPointerRectPos = Vector3.zero;
		Vector3 prevHorPointerRectPosRaw = Vector3.zero, prevVerPointerRectPosRaw = Vector3.zero;

		Vector3 deltaHorPointerRectPos = Vector3.zero, deltaVerPointerRectPos = Vector3.zero;
		Vector3 deltaHorPointerRectPosRaw = Vector3.zero, deltaVerPointerRectPosRaw = Vector3.zero;

		Vector3 realHorTouchPosRectTrans = Vector3.zero, realVerTouchPosRectTrans = Vector3.zero;
		Vector3 realHorTouchPosRawRectTrans = Vector3.zero, realVerTouchPosRawRectTrans = Vector3.zero;

		//touch indices
		int touchWithinHorRectTransform, touchWithinVerRectTransform;
		int touchWithinHorRawRectTransform, touchWithinVerRawRectTransform;

		int touchWithinRectTransform;
       
		bool CheckIfAnyTouchInAllTouchesIsInsideRectArea(Rect RectArea, ref int currentTouchIndex)
		{
			bool returnBool = false;

			currentTouchIndex = 0;

			for (int i = 0; i < Input.touchCount; i++) 
			{
				Vector2 realTouchPos = new Vector2 (Input.touches[i].position.x, Screen.height - Input.touches[i].position.y);

				if (RectArea.Contains(realTouchPos)) 
				{
					currentTouchIndex = i;
					returnBool = true;
					break;
				}
			}

			return returnBool;
		}

		public static float EvaluateHorizontalSwipeStatic (bool allowSwipeOverUI, string ignoreUILayer)
		{
			float val = 0f;

			if (allowSwipeOverUI) 
			{
				if (Input.touchCount > 0)
				{
					if (Input.GetTouch (Input.touchCount - 1).phase == TouchPhase.Began) 
					{
						prevHorPointerPosStatic = new Vector3 (Input.GetTouch (Input.touchCount - 1).position.x, Input.GetTouch (Input.touchCount - 1).position.y, 0f);
					} 
					else if (Input.GetTouch (Input.touchCount - 1).phase == TouchPhase.Moved) 
					{
						realHorTouchPos = new Vector3 (Input.GetTouch (Input.touchCount - 1).position.x, Input.GetTouch (Input.touchCount - 1).position.y, 0f);

						deltaHorPointerPosStatic = (realHorTouchPos - prevHorPointerPosStatic).normalized;

						//This is a simplification of the Dot Product between 
						//the Mouse Direction Vector and the World Right Vector
						val = deltaHorPointerPosStatic.x;
					} 
					else if (Input.GetTouch (Input.touchCount - 1).phase == TouchPhase.Ended || Input.GetTouch (Input.touchCount - 1).phase == TouchPhase.Canceled) 
					{
						prevHorPointerPosStatic = new Vector3 (Input.GetTouch (Input.touchCount - 1).position.x, Input.GetTouch (Input.touchCount - 1).position.y, 0f);
					}

					prevHorPointerPosStatic = new Vector3 (Input.GetTouch(Input.touchCount - 1).position.x, Input.GetTouch(Input.touchCount - 1).position.y, 0f);
				}

				prevHorPointerPosStatic = new Vector3 (Input.GetTouch(Input.touchCount - 1).position.x, Input.GetTouch(Input.touchCount - 1).position.y, 0f);
			} 
			else 
			{
				try
				{
					if (Input.touchCount > 0)
					{
						if (Input.GetTouch (Input.touchCount - 1).phase == TouchPhase.Began && !CanvasGraphicsRaycaster.Instance.IsPointerOverUI(ignoreUILayer, Input.GetTouch (Input.touchCount - 1).position)) 
						{
							prevHorPointerPosStatic = new Vector3 (Input.GetTouch (Input.touchCount - 1).position.x, Input.GetTouch (Input.touchCount - 1).position.y, 0f);
						} 
						else if (Input.GetTouch (Input.touchCount - 1).phase == TouchPhase.Moved && !CanvasGraphicsRaycaster.Instance.IsPointerOverUI(ignoreUILayer, Input.GetTouch (Input.touchCount - 1).position)) 
						{
							realHorTouchPos = new Vector3 (Input.GetTouch (Input.touchCount - 1).position.x, Input.GetTouch (Input.touchCount - 1).position.y, 0f);

							deltaHorPointerPosStatic = (realHorTouchPos - prevHorPointerPosStatic).normalized;

							//This is a simplification of the Dot Product between 
							//the Mouse Direction Vector and the World Right Vector
							val = deltaHorPointerPosStatic.x;
						} 
						else if (Input.GetTouch (Input.touchCount - 1).phase == TouchPhase.Ended || Input.GetTouch (Input.touchCount - 1).phase == TouchPhase.Canceled && 
							!CanvasGraphicsRaycaster.Instance.IsPointerOverUI(ignoreUILayer, Input.GetTouch (Input.touchCount - 1).position)) 
						{
							prevHorPointerPosStatic = new Vector3 (Input.GetTouch (Input.touchCount - 1).position.x, Input.GetTouch (Input.touchCount - 1).position.y, 0f);
						}

						prevHorPointerPosStatic = new Vector3 (Input.GetTouch(Input.touchCount - 1).position.x, Input.GetTouch(Input.touchCount - 1).position.y, 0f);
					}

					prevHorPointerPosStatic = new Vector3 (Input.GetTouch(Input.touchCount - 1).position.x, Input.GetTouch(Input.touchCount - 1).position.y, 0f);
				} 
				catch 
				{
					Debug.LogError ("CanvasGraphicsRaycaster script needs to the Attached to the Canvas Object");
					Debug.LogError ("Also make sure there is an Active Event System Object in your Hierarchy");
				}
			}

			return val;
		}

		public static float EvaluateHorizontalSwipeRawStatic (bool allowSwipeOverUI, string ignoreUILayer)
		{
			float val = 0f;

			if (allowSwipeOverUI) 
			{
				if (Input.touchCount > 0)
				{
					if (Input.GetTouch (Input.touchCount - 1).phase == TouchPhase.Began) 
					{
						prevHorPointerPosRawStatic = new Vector3 (Input.GetTouch (Input.touchCount - 1).position.x, Input.GetTouch (Input.touchCount - 1).position.y, 0f);
					} 
					else if (Input.GetTouch (Input.touchCount - 1).phase == TouchPhase.Moved) 
					{
						realHorTouchPosRaw = new Vector3 (Input.GetTouch (Input.touchCount - 1).position.x, Input.GetTouch (Input.touchCount - 1).position.y, 0f);

						deltaHorPointerPosRawStatic = realHorTouchPosRaw - prevHorPointerPosRawStatic;

						val = deltaHorPointerPosRawStatic.x;
					} 
					else if (Input.GetTouch (Input.touchCount - 1).phase == TouchPhase.Ended || Input.GetTouch (Input.touchCount - 1).phase == TouchPhase.Canceled) 
					{
						prevHorPointerPosRawStatic = new Vector3 (Input.GetTouch (Input.touchCount - 1).position.x, Input.GetTouch (Input.touchCount - 1).position.y, 0f);
					}

					prevHorPointerPosRawStatic = new Vector3 (Input.GetTouch(Input.touchCount - 1).position.x, Input.GetTouch(Input.touchCount - 1).position.y, 0f);
				}

				prevHorPointerPosRawStatic = new Vector3 (Input.GetTouch(Input.touchCount - 1).position.x, Input.GetTouch(Input.touchCount - 1).position.y, 0f);
			} 
			else 
			{
				try 
				{
					if (Input.touchCount > 0)
					{
						if (Input.GetTouch (Input.touchCount - 1).phase == TouchPhase.Began && !CanvasGraphicsRaycaster.Instance.IsPointerOverUI(ignoreUILayer, Input.GetTouch (Input.touchCount - 1).position)) 
						{
							prevHorPointerPosRawStatic = new Vector3 (Input.GetTouch (Input.touchCount - 1).position.x, Input.GetTouch (Input.touchCount - 1).position.y, 0f);
						} 
						else if (Input.GetTouch (Input.touchCount - 1).phase == TouchPhase.Moved && !CanvasGraphicsRaycaster.Instance.IsPointerOverUI(ignoreUILayer, Input.GetTouch (Input.touchCount - 1).position)) 
						{
							realHorTouchPosRaw = new Vector3 (Input.GetTouch (Input.touchCount - 1).position.x, Input.GetTouch (Input.touchCount - 1).position.y, 0f);

							deltaHorPointerPosRawStatic = realHorTouchPosRaw - prevHorPointerPosRawStatic;

							val = deltaHorPointerPosRawStatic.x;
						} 
						else if (Input.GetTouch (Input.touchCount - 1).phase == TouchPhase.Ended || Input.GetTouch (Input.touchCount - 1).phase == TouchPhase.Canceled && 
							!CanvasGraphicsRaycaster.Instance.IsPointerOverUI(ignoreUILayer, Input.GetTouch (Input.touchCount - 1).position)) 
						{
							prevHorPointerPosRawStatic = new Vector3 (Input.GetTouch (Input.touchCount - 1).position.x, Input.GetTouch (Input.touchCount - 1).position.y, 0f);
						}

						prevHorPointerPosRawStatic = new Vector3 (Input.GetTouch(Input.touchCount - 1).position.x, Input.GetTouch(Input.touchCount - 1).position.y, 0f);
					}

					prevHorPointerPosRawStatic = new Vector3 (Input.GetTouch(Input.touchCount - 1).position.x, Input.GetTouch(Input.touchCount - 1).position.y, 0f);
				} 
				catch
				{
					Debug.LogError ("CanvasGraphicsRaycaster script needs to the Attached to the Canvas Object");
					Debug.LogError ("Also make sure there is an Active Event System Object in your Hierarchy");
				}	
			}
				
			return Mathf.Clamp (val, -1f, 1f);
		}

		public static float EvaluateVerticalSwipeStatic (bool allowSwipeOverUI, string ignoreUILayer)
		{
			float val = 0f;

			if (allowSwipeOverUI) 
			{
				if (Input.touchCount > 0)
				{
					if (Input.GetTouch (Input.touchCount - 1).phase == TouchPhase.Began) 
					{
						prevVerPointerPosStatic = new Vector3 (Input.GetTouch (Input.touchCount - 1).position.x, Input.GetTouch (Input.touchCount - 1).position.y, 0f);
					} 
					else if (Input.GetTouch (Input.touchCount - 1).phase == TouchPhase.Moved) 
					{
						realVerTouchPos = new Vector3 (Input.GetTouch (Input.touchCount - 1).position.x, Input.GetTouch (Input.touchCount - 1).position.y, 0f);

						deltaVerPointerPosStatic = (realVerTouchPos - prevVerPointerPosStatic).normalized;

						//This is a simplification of the Dot Product between 
						//the Mouse Direction Vector and the World Up Vector
						val = deltaVerPointerPosStatic.y;
					} 
					else if (Input.GetTouch (Input.touchCount - 1).phase == TouchPhase.Ended || Input.GetTouch (Input.touchCount - 1).phase == TouchPhase.Canceled) 
					{
						prevVerPointerPosStatic = new Vector3 (Input.GetTouch (Input.touchCount - 1).position.x, Input.GetTouch (Input.touchCount - 1).position.y, 0f);
					}

					prevVerPointerPosStatic = new Vector3 (Input.GetTouch(Input.touchCount - 1).position.x, Input.GetTouch(Input.touchCount - 1).position.y, 0f);
				}

				prevVerPointerPosStatic = new Vector3 (Input.GetTouch(Input.touchCount - 1).position.x, Input.GetTouch(Input.touchCount - 1).position.y, 0f);
			} 
			else 
			{
				try 
				{
					if (Input.touchCount > 0)
					{
						if (Input.GetTouch (Input.touchCount - 1).phase == TouchPhase.Began && !CanvasGraphicsRaycaster.Instance.IsPointerOverUI(ignoreUILayer, Input.GetTouch (Input.touchCount - 1).position)) 
						{
							prevVerPointerPosStatic = new Vector3 (Input.GetTouch (Input.touchCount - 1).position.x, Input.GetTouch (Input.touchCount - 1).position.y, 0f);
						} 
						else if (Input.GetTouch (Input.touchCount - 1).phase == TouchPhase.Moved && !CanvasGraphicsRaycaster.Instance.IsPointerOverUI(ignoreUILayer, Input.GetTouch (Input.touchCount - 1).position)) 
						{
							realVerTouchPos = new Vector3 (Input.GetTouch (Input.touchCount - 1).position.x, Input.GetTouch (Input.touchCount - 1).position.y, 0f);

							deltaVerPointerPosStatic = (realVerTouchPos - prevVerPointerPosStatic).normalized;

							//This is a simplification of the Dot Product between 
							//the Mouse Direction Vector and the World Up Vector
							val = deltaVerPointerPosStatic.y;
						} 
						else if (Input.GetTouch (Input.touchCount - 1).phase == TouchPhase.Ended || Input.GetTouch (Input.touchCount - 1).phase == TouchPhase.Canceled && 
							!CanvasGraphicsRaycaster.Instance.IsPointerOverUI(ignoreUILayer, Input.GetTouch (Input.touchCount - 1).position)) 
						{
							prevVerPointerPosStatic = new Vector3 (Input.GetTouch (Input.touchCount - 1).position.x, Input.GetTouch (Input.touchCount - 1).position.y, 0f);
						}

						prevVerPointerPosStatic = new Vector3 (Input.GetTouch(Input.touchCount - 1).position.x, Input.GetTouch(Input.touchCount - 1).position.y, 0f);
					}

					prevVerPointerPosStatic = new Vector3 (Input.GetTouch(Input.touchCount - 1).position.x, Input.GetTouch(Input.touchCount - 1).position.y, 0f);
				} 
				catch
				{
					Debug.LogError ("CanvasGraphicsRaycaster script needs to the Attached to the Canvas Object");
					Debug.LogError ("Also make sure there is an Active Event System Object in your Hierarchy");
				}
			}

			return val;
		}

		public static float EvaluateVerticalSwipeRawStatic (bool allowSwipeOverUI, string ignoreUILayer)
		{
			float val = 0f;

			if (allowSwipeOverUI) 
			{
				if (Input.touchCount > 0)
				{
					if (Input.GetTouch (Input.touchCount - 1).phase == TouchPhase.Began) 
					{
						prevVerPointerPosRawStatic = new Vector3 (Input.GetTouch (Input.touchCount - 1).position.x, Input.GetTouch (Input.touchCount - 1).position.y, 0f);
					} 
					else if (Input.GetTouch (Input.touchCount - 1).phase == TouchPhase.Moved) 
					{
						realVerTouchPosRaw = new Vector3 (Input.GetTouch (Input.touchCount - 1).position.x, Input.GetTouch (Input.touchCount - 1).position.y, 0f);

						deltaVerPointerPosRawStatic = realVerTouchPosRaw - prevVerPointerPosRawStatic;

						val = deltaVerPointerPosRawStatic.y;
					} 
					else if (Input.GetTouch (Input.touchCount - 1).phase == TouchPhase.Ended || Input.GetTouch (Input.touchCount - 1).phase == TouchPhase.Canceled) 
					{
						prevVerPointerPosRawStatic = new Vector3 (Input.GetTouch (Input.touchCount - 1).position.x, Input.GetTouch (Input.touchCount - 1).position.y, 0f);
					}

					prevVerPointerPosRawStatic = new Vector3 (Input.GetTouch(Input.touchCount - 1).position.x, Input.GetTouch(Input.touchCount - 1).position.y, 0f);
				}

				prevVerPointerPosRawStatic = new Vector3 (Input.GetTouch(Input.touchCount - 1).position.x, Input.GetTouch(Input.touchCount - 1).position.y, 0f);
			} 
			else 
			{
				try 
				{
					if (Input.touchCount > 0)
					{
						if (Input.GetTouch (Input.touchCount - 1).phase == TouchPhase.Began && !CanvasGraphicsRaycaster.Instance.IsPointerOverUI(ignoreUILayer, Input.GetTouch (Input.touchCount - 1).position)) 
						{
							prevVerPointerPosRawStatic = new Vector3 (Input.GetTouch (Input.touchCount - 1).position.x, Input.GetTouch (Input.touchCount - 1).position.y, 0f);
						} 
						else if (Input.GetTouch (Input.touchCount - 1).phase == TouchPhase.Moved && !CanvasGraphicsRaycaster.Instance.IsPointerOverUI(ignoreUILayer, Input.GetTouch (Input.touchCount - 1).position)) 
						{
							realVerTouchPosRaw = new Vector3 (Input.GetTouch (Input.touchCount - 1).position.x, Input.GetTouch (Input.touchCount - 1).position.y, 0f);

							deltaVerPointerPosRawStatic = realVerTouchPosRaw - prevVerPointerPosRawStatic;

							val = deltaVerPointerPosRawStatic.y;
						} 
						else if (Input.GetTouch (Input.touchCount - 1).phase == TouchPhase.Ended || Input.GetTouch (Input.touchCount - 1).phase == TouchPhase.Canceled && 
							!CanvasGraphicsRaycaster.Instance.IsPointerOverUI(ignoreUILayer, Input.GetTouch (Input.touchCount - 1).position)) 
						{
							prevVerPointerPosRawStatic = new Vector3 (Input.GetTouch (Input.touchCount - 1).position.x, Input.GetTouch (Input.touchCount - 1).position.y, 0f);
						}

						prevVerPointerPosRawStatic = new Vector3 (Input.GetTouch(Input.touchCount - 1).position.x, Input.GetTouch(Input.touchCount - 1).position.y, 0f);
					}

					prevVerPointerPosRawStatic = new Vector3 (Input.GetTouch(Input.touchCount - 1).position.x, Input.GetTouch(Input.touchCount - 1).position.y, 0f);
				} 
				catch 
				{
					Debug.LogError ("CanvasGraphicsRaycaster script needs to the Attached to the Canvas Object");
					Debug.LogError ("Also make sure there is an Active Event System Object in your Hierarchy");
				}
			}

			return Mathf.Clamp (val, -1f, 1f);
		}
			
		public override float EvaluateHorizontalSwipeInRectTrans (RectTransform rectTrans, bool allowSwipeOverUI, string ignoreUILayer)
		{
			float val = 0f;
			Rect rectArea = GetScreenCoord (rectTrans);

			if (!CheckIfAnyTouchInAllTouchesIsInsideRectArea (rectArea, ref touchWithinHorRectTransform))
				return 0f;

			if (allowSwipeOverUI) 
			{
				if (Input.touchCount > 0) 
				{
					if (Input.GetTouch(touchWithinHorRectTransform).phase == TouchPhase.Began) 
					{
						prevHorPointerRectPos = new Vector3 (Input.GetTouch(touchWithinHorRectTransform).position.x, Input.GetTouch(touchWithinHorRectTransform).position.y, 0f);
					}
					else if (Input.GetTouch(touchWithinHorRectTransform).phase == TouchPhase.Moved) 
					{
						realHorTouchPosRectTrans = new Vector3 (Input.GetTouch(touchWithinHorRectTransform).position.x, Input.GetTouch(touchWithinHorRectTransform).position.y, 0f);

						deltaHorPointerRectPos = (realHorTouchPosRectTrans - prevHorPointerRectPos).normalized;

						val = deltaHorPointerRectPos.x;
					}
					else if (Input.GetTouch(touchWithinHorRectTransform).phase == TouchPhase.Ended || Input.GetTouch(touchWithinHorRectTransform).phase == TouchPhase.Canceled) 
					{
						prevHorPointerRectPos = new Vector3 (Input.GetTouch(touchWithinHorRectTransform).position.x, Input.GetTouch(touchWithinHorRectTransform).position.y, 0f);
					}

					prevHorPointerRectPos = new Vector3 (Input.GetTouch(touchWithinHorRectTransform).position.x, Input.GetTouch(touchWithinHorRectTransform).position.y, 0f);
				}

				prevHorPointerRectPos = new Vector3 (Input.GetTouch(touchWithinHorRectTransform).position.x, Input.GetTouch(touchWithinHorRectTransform).position.y, 0f);
			} 
			else 
			{
				try 
				{
					if (Input.touchCount > 0) 
					{
						if (Input.GetTouch(touchWithinHorRectTransform).phase == TouchPhase.Began && !CanvasGraphicsRaycaster.Instance.IsPointerOverUI(ignoreUILayer, Input.GetTouch (touchWithinHorRectTransform).position)) 
						{
							prevHorPointerRectPos = new Vector3 (Input.GetTouch(touchWithinHorRectTransform).position.x, Input.GetTouch(touchWithinHorRectTransform).position.y, 0f);
						}
						else if (Input.GetTouch(touchWithinHorRectTransform).phase == TouchPhase.Moved && !CanvasGraphicsRaycaster.Instance.IsPointerOverUI(ignoreUILayer, Input.GetTouch (touchWithinHorRectTransform).position)) 
						{
							realHorTouchPosRectTrans = new Vector3 (Input.GetTouch(touchWithinHorRectTransform).position.x, Input.GetTouch(touchWithinHorRectTransform).position.y, 0f);

							deltaHorPointerRectPos = (realHorTouchPosRectTrans - prevHorPointerRectPos).normalized;

							val = deltaHorPointerRectPos.x;
						}
						else if (Input.GetTouch(touchWithinHorRectTransform).phase == TouchPhase.Ended || Input.GetTouch(touchWithinHorRectTransform).phase == TouchPhase.Canceled && 
							!CanvasGraphicsRaycaster.Instance.IsPointerOverUI(ignoreUILayer, Input.GetTouch (touchWithinHorRectTransform).position)) 
						{
							prevHorPointerRectPos = new Vector3 (Input.GetTouch(touchWithinHorRectTransform).position.x, Input.GetTouch(touchWithinHorRectTransform).position.y, 0f);
						}

						prevHorPointerRectPos = new Vector3 (Input.GetTouch(touchWithinHorRectTransform).position.x, Input.GetTouch(touchWithinHorRectTransform).position.y, 0f);
					}

					prevHorPointerRectPos = new Vector3 (Input.GetTouch(touchWithinHorRectTransform).position.x, Input.GetTouch(touchWithinHorRectTransform).position.y, 0f);
				} 
				catch 
				{
					Debug.LogError ("CanvasGraphicsRaycaster script needs to the Attached to the Canvas Object");
					Debug.LogError ("Also make sure there is an Active Event System Object in your Hierarchy");
				}
			}
				
			return val;
		}

		public override float EvaluateHorizontalSwipeRawInRectTrans (RectTransform rectTrans, bool allowSwipeOverUI, string ignoreUILayer)
		{
			float val = 0f;
			Rect rectArea = GetScreenCoord (rectTrans);

			if (!CheckIfAnyTouchInAllTouchesIsInsideRectArea (rectArea, ref touchWithinHorRawRectTransform))
				return 0f;

			if (allowSwipeOverUI) 
			{
				if (Input.touchCount > 0) 
				{
					if (Input.GetTouch(touchWithinHorRawRectTransform).phase == TouchPhase.Began) 
					{
						prevHorPointerRectPosRaw = new Vector3 (Input.GetTouch(touchWithinHorRawRectTransform).position.x, Input.GetTouch(touchWithinHorRawRectTransform).position.y, 0f);
					}
					else if (Input.GetTouch(touchWithinHorRawRectTransform).phase == TouchPhase.Moved) 
					{
						realHorTouchPosRawRectTrans = new Vector3 (Input.GetTouch(touchWithinHorRawRectTransform).position.x, Input.GetTouch(touchWithinHorRawRectTransform).position.y, 0f);

						deltaHorPointerRectPosRaw = realHorTouchPosRawRectTrans - prevHorPointerRectPosRaw;

						val = deltaHorPointerRectPosRaw.x;
					}
					else if (Input.GetTouch(touchWithinHorRawRectTransform).phase == TouchPhase.Ended || Input.GetTouch(touchWithinHorRawRectTransform).phase == TouchPhase.Canceled) 
					{
						prevHorPointerRectPosRaw = new Vector3 (Input.GetTouch(touchWithinHorRawRectTransform).position.x, Input.GetTouch(touchWithinHorRawRectTransform).position.y, 0f);
					}

					prevHorPointerRectPosRaw = new Vector3 (Input.GetTouch(touchWithinHorRawRectTransform).position.x, Input.GetTouch(touchWithinHorRawRectTransform).position.y, 0f);
				}

				prevHorPointerRectPosRaw = new Vector3 (Input.GetTouch(touchWithinHorRawRectTransform).position.x, Input.GetTouch(touchWithinHorRawRectTransform).position.y, 0f);
			} 
			else 
			{
				try 
				{
					if (Input.touchCount > 0) 
					{
						if (Input.GetTouch(touchWithinHorRawRectTransform).phase == TouchPhase.Began && !CanvasGraphicsRaycaster.Instance.IsPointerOverUI(ignoreUILayer, Input.GetTouch (touchWithinHorRawRectTransform).position)) 
						{
							prevHorPointerRectPosRaw = new Vector3 (Input.GetTouch(touchWithinHorRawRectTransform).position.x, Input.GetTouch(touchWithinHorRawRectTransform).position.y, 0f);
						}
						else if (Input.GetTouch(touchWithinHorRawRectTransform).phase == TouchPhase.Moved && !CanvasGraphicsRaycaster.Instance.IsPointerOverUI(ignoreUILayer, Input.GetTouch (touchWithinHorRawRectTransform).position)) 
						{
							realHorTouchPosRawRectTrans = new Vector3 (Input.GetTouch(touchWithinHorRawRectTransform).position.x, Input.GetTouch(touchWithinHorRawRectTransform).position.y, 0f);

							deltaHorPointerRectPosRaw = realHorTouchPosRawRectTrans - prevHorPointerRectPosRaw;

							val = deltaHorPointerRectPosRaw.x;
						}
						else if (Input.GetTouch(touchWithinHorRawRectTransform).phase == TouchPhase.Ended || Input.GetTouch(touchWithinHorRawRectTransform).phase == TouchPhase.Canceled && 
							!CanvasGraphicsRaycaster.Instance.IsPointerOverUI(ignoreUILayer, Input.GetTouch (touchWithinHorRawRectTransform).position)) 
						{
							prevHorPointerRectPosRaw = new Vector3 (Input.GetTouch(touchWithinHorRawRectTransform).position.x, Input.GetTouch(touchWithinHorRawRectTransform).position.y, 0f);
						}

						prevHorPointerRectPosRaw = new Vector3 (Input.GetTouch(touchWithinHorRawRectTransform).position.x, Input.GetTouch(touchWithinHorRawRectTransform).position.y, 0f);
					}

					prevHorPointerRectPosRaw = new Vector3 (Input.GetTouch(touchWithinHorRawRectTransform).position.x, Input.GetTouch(touchWithinHorRawRectTransform).position.y, 0f);
				} 
				catch
				{
					Debug.LogError ("CanvasGraphicsRaycaster script needs to the Attached to the Canvas Object");
					Debug.LogError ("Also make sure there is an Active Event System Object in your Hierarchy");
				}
			}

			return Mathf.Clamp (val, -1f, 1f);
		}
			
		public override float EvaluateVerticalSwipeInRectTrans (RectTransform rectTrans, bool allowSwipeOverUI, string ignoreUILayer)
		{
			float val = 0f;
			Rect rectArea = GetScreenCoord (rectTrans);

			if (!CheckIfAnyTouchInAllTouchesIsInsideRectArea (rectArea, ref touchWithinVerRectTransform))
				return 0f;

			if (allowSwipeOverUI) 
			{
				if (Input.touchCount > 0) 
				{
					if (Input.GetTouch(touchWithinVerRectTransform).phase == TouchPhase.Began) 
					{
						prevVerPointerRectPos = new Vector3 (Input.GetTouch(touchWithinVerRectTransform).position.x, Input.GetTouch(touchWithinVerRectTransform).position.y, 0f);
					}
					else if (Input.GetTouch(touchWithinVerRectTransform).phase == TouchPhase.Moved) 
					{
						realVerTouchPosRectTrans = new Vector3 (Input.GetTouch(touchWithinVerRectTransform).position.x, Input.GetTouch(touchWithinVerRectTransform).position.y, 0f);

						deltaVerPointerRectPos = (realVerTouchPosRectTrans - prevVerPointerRectPos).normalized;

						val = deltaVerPointerRectPos.y;
					}
					else if (Input.GetTouch(touchWithinVerRectTransform).phase == TouchPhase.Ended || Input.GetTouch(touchWithinVerRectTransform).phase == TouchPhase.Canceled) 
					{
						prevVerPointerRectPos = new Vector3 (Input.GetTouch(touchWithinVerRectTransform).position.x, Input.GetTouch(touchWithinVerRectTransform).position.y, 0f);
					}

					prevVerPointerRectPos = new Vector3 (Input.GetTouch(touchWithinVerRectTransform).position.x, Input.GetTouch(touchWithinVerRectTransform).position.y, 0f);
				}

				prevVerPointerRectPos = new Vector3 (Input.GetTouch(touchWithinVerRectTransform).position.x, Input.GetTouch(touchWithinVerRectTransform).position.y, 0f);
			} 
			else
			{
				try 
				{
					if (Input.touchCount > 0) 
					{
						if (Input.GetTouch(touchWithinVerRectTransform).phase == TouchPhase.Began && !CanvasGraphicsRaycaster.Instance.IsPointerOverUI(ignoreUILayer, Input.GetTouch (touchWithinVerRectTransform).position)) 
						{
							prevVerPointerRectPos = new Vector3 (Input.GetTouch(touchWithinVerRectTransform).position.x, Input.GetTouch(touchWithinVerRectTransform).position.y, 0f);
						}
						else if (Input.GetTouch(touchWithinVerRectTransform).phase == TouchPhase.Moved && !CanvasGraphicsRaycaster.Instance.IsPointerOverUI(ignoreUILayer, Input.GetTouch (touchWithinVerRectTransform).position)) 
						{
							realVerTouchPosRectTrans = new Vector3 (Input.GetTouch(touchWithinVerRectTransform).position.x, Input.GetTouch(touchWithinVerRectTransform).position.y, 0f);

							deltaVerPointerRectPos = (realVerTouchPosRectTrans - prevVerPointerRectPos).normalized;

							val = deltaVerPointerRectPos.y;
						}
						else if (Input.GetTouch(touchWithinVerRectTransform).phase == TouchPhase.Ended || Input.GetTouch(touchWithinVerRectTransform).phase == TouchPhase.Canceled && 
							!CanvasGraphicsRaycaster.Instance.IsPointerOverUI(ignoreUILayer, Input.GetTouch (touchWithinVerRectTransform).position)) 
						{
							prevVerPointerRectPos = new Vector3 (Input.GetTouch(touchWithinVerRectTransform).position.x, Input.GetTouch(touchWithinVerRectTransform).position.y, 0f);
						}

						prevVerPointerRectPos = new Vector3 (Input.GetTouch(touchWithinVerRectTransform).position.x, Input.GetTouch(touchWithinVerRectTransform).position.y, 0f);
					}

					prevVerPointerRectPos = new Vector3 (Input.GetTouch(touchWithinVerRectTransform).position.x, Input.GetTouch(touchWithinVerRectTransform).position.y, 0f);
				} 
				catch 
				{
					Debug.LogError ("CanvasGraphicsRaycaster script needs to the Attached to the Canvas Object");
					Debug.LogError ("Also make sure there is an Active Event System Object in your Hierarchy");
				}
			}
				
			return val;
		}

		public override float EvaluateVerticalSwipeRawInRectTrans (RectTransform rectTrans, bool allowSwipeOverUI, string ignoreUILayer)
		{
			float val = 0f;
			Rect rectArea = GetScreenCoord (rectTrans);

			if (!CheckIfAnyTouchInAllTouchesIsInsideRectArea (rectArea, ref touchWithinVerRawRectTransform))
				return 0f;

			if (allowSwipeOverUI) 
			{
				if (Input.touchCount > 0) 
				{
					if (Input.GetTouch(touchWithinVerRawRectTransform).phase == TouchPhase.Began) 
					{
						prevVerPointerRectPosRaw = new Vector3 (Input.GetTouch(touchWithinVerRawRectTransform).position.x, Input.GetTouch(touchWithinVerRawRectTransform).position.y, 0f);
					}
					else if (Input.GetTouch(touchWithinVerRawRectTransform).phase == TouchPhase.Moved) 
					{
						realVerTouchPosRawRectTrans = new Vector3 (Input.GetTouch(touchWithinVerRawRectTransform).position.x, Input.GetTouch(touchWithinVerRawRectTransform).position.y, 0f);

						deltaVerPointerRectPosRaw = realVerTouchPosRawRectTrans - prevVerPointerRectPosRaw;

						val = deltaVerPointerRectPosRaw.y;
					}
					else if ((Input.GetTouch(touchWithinVerRawRectTransform).phase == TouchPhase.Ended || Input.GetTouch(touchWithinVerRawRectTransform).phase == TouchPhase.Canceled)) 
					{
						prevVerPointerRectPosRaw = new Vector3 (Input.GetTouch(touchWithinVerRawRectTransform).position.x, Input.GetTouch(touchWithinVerRawRectTransform).position.y, 0f);
					}

					prevVerPointerRectPosRaw = new Vector3 (Input.GetTouch(touchWithinVerRawRectTransform).position.x, Input.GetTouch(touchWithinVerRawRectTransform).position.y, 0f);
				}

				prevVerPointerRectPosRaw = new Vector3 (Input.GetTouch(touchWithinVerRawRectTransform).position.x, Input.GetTouch(touchWithinVerRawRectTransform).position.y, 0f);
			} 
			else 
			{
				try 
				{
					if (Input.touchCount > 0) 
					{
						if (Input.GetTouch(touchWithinVerRawRectTransform).phase == TouchPhase.Began && !CanvasGraphicsRaycaster.Instance.IsPointerOverUI(ignoreUILayer, Input.GetTouch (touchWithinVerRawRectTransform).position)) 
						{
							prevVerPointerRectPosRaw = new Vector3 (Input.GetTouch(touchWithinVerRawRectTransform).position.x, Input.GetTouch(touchWithinVerRawRectTransform).position.y, 0f);
						}
						else if (Input.GetTouch(touchWithinVerRawRectTransform).phase == TouchPhase.Moved && !CanvasGraphicsRaycaster.Instance.IsPointerOverUI(ignoreUILayer, Input.GetTouch (touchWithinVerRawRectTransform).position)) 
						{
							realVerTouchPosRawRectTrans = new Vector3 (Input.GetTouch(touchWithinVerRawRectTransform).position.x, Input.GetTouch(touchWithinVerRawRectTransform).position.y, 0f);

							deltaVerPointerRectPosRaw = realVerTouchPosRawRectTrans - prevVerPointerRectPosRaw;

							val = deltaVerPointerRectPosRaw.y;
						}
						else if ((Input.GetTouch(touchWithinVerRawRectTransform).phase == TouchPhase.Ended || Input.GetTouch(touchWithinVerRawRectTransform).phase == TouchPhase.Canceled) && 
							!CanvasGraphicsRaycaster.Instance.IsPointerOverUI(ignoreUILayer, Input.GetTouch (touchWithinVerRawRectTransform).position)) 
						{
							prevVerPointerRectPosRaw = new Vector3 (Input.GetTouch(touchWithinVerRawRectTransform).position.x, Input.GetTouch(touchWithinVerRawRectTransform).position.y, 0f);
						}

						prevVerPointerRectPosRaw = new Vector3 (Input.GetTouch(touchWithinVerRawRectTransform).position.x, Input.GetTouch(touchWithinVerRawRectTransform).position.y, 0f);
					}

					prevVerPointerRectPosRaw = new Vector3 (Input.GetTouch(touchWithinVerRawRectTransform).position.x, Input.GetTouch(touchWithinVerRawRectTransform).position.y, 0f);
				} 
				catch 
				{
					Debug.LogError ("CanvasGraphicsRaycaster script needs to the Attached to the Canvas Object");
					Debug.LogError ("Also make sure there is an Active Event System Object in your Hierarchy");
				}
			}

			return Mathf.Clamp (val, -1f, 1f);
		}

		public override float EvaluateTouch (float axisVal, ref bool isTouched, bool allowTouchOverUI, string ignoreUILayer)
		{
			float val = 0f;
			isTouched = true;

			if (allowTouchOverUI) 
			{
				if (Input.touchCount > 0) 
				{
					val = axisVal;
					isTouched = true;
				}
			} 
			else 
			{
				try 
				{
					if (Input.touchCount > 0 && !CanvasGraphicsRaycaster.Instance.IsPointerOverUI(ignoreUILayer, Input.GetTouch (Input.touchCount - 1).position)) 
					{
						val = axisVal;
						isTouched = true;
					}
				} 
				catch 
				{
					Debug.LogError ("CanvasGraphicsRaycaster script needs to the Attached to the Canvas Object");
					Debug.LogError ("Also make sure there is an Active Event System Object in your Hierarchy");
				}
			}
		
			return val;
		}

		public override float EvaluateTouchInRectTrans (RectTransform rectTrans, float axisVal, ref bool isTouched, bool allowTouchOverUI, string ignoreUILayer)
		{
			float val = 0f;
			isTouched = false;

			Rect rectArea = GetScreenCoord (rectTrans);

			if (!CheckIfAnyTouchInAllTouchesIsInsideRectArea (rectArea, ref touchWithinRectTransform))
				return 0f;

			if (allowTouchOverUI) 
			{
				if (Input.touchCount > 0 && CheckIfAnyTouchInAllTouchesIsInsideRectArea (rectArea, ref touchWithinRectTransform)) 
				{
					val = axisVal;
					isTouched = true;
				}
			} 
			else 
			{
				try
				{
					if (Input.touchCount > 0 && CheckIfAnyTouchInAllTouchesIsInsideRectArea (rectArea, ref touchWithinRectTransform) && 
						!CanvasGraphicsRaycaster.Instance.IsPointerOverUI(ignoreUILayer, Input.GetTouch (touchWithinRectTransform).position)) 
					{
						val = axisVal;
						isTouched = true;
					}
				} 
				catch 
				{
					Debug.LogError ("CanvasGraphicsRaycaster script needs to the Attached to the Canvas Object");
					Debug.LogError ("Also make sure there is an Active Event System Object in your Hierarchy");
				}
			}
				
			return val;
		}
	}
}
