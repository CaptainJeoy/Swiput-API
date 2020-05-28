using UnityEngine;

namespace SwiputAPI.CrossPlatform
{
	public class StandaloneSwiput : VirtualSwiput
	{
		//Static
		static Vector3 prevHorPointerPosStatic = Vector3.zero, prevVerPointerPosStatic = Vector3.zero;
		static Vector3 prevHorPointerPosRawStatic = Vector3.zero, prevVerPointerPosRawStatic = Vector3.zero;

		static Vector3 deltaHorPointerPosStatic = Vector3.zero, deltaVerPointerPosStatic = Vector3.zero;
		static Vector3 deltaHorPointerPosRawStatic = Vector3.zero, deltaVerPointerPosRawStatic = Vector3.zero;


		//Swipe In Rect
		Vector3 prevHorPointerRectPos = Vector3.zero, prevVerPointerRectPos = Vector3.zero;
		Vector3 prevHorPointerRectRawPos = Vector3.zero, prevVerPointerRectRawPos = Vector3.zero;

		Vector3 deltaHorPointerRectPos = Vector3.zero, deltaVerPointerRectPos = Vector3.zero;
		Vector3 deltaHorPointerRectRawPos = Vector3.zero, deltaVerPointerRectRawPos = Vector3.zero;

		public static float EvaluateHorizontalSwipeStatic (bool allowSwipeOverUI, string ignoreUILayer)		
		{
			Vector3 realMousePos = Input.mousePosition;
			float val = 0f;

			if (allowSwipeOverUI)
			{
				if (Input.GetMouseButton (0)) 
				{
					deltaHorPointerPosStatic = (realMousePos - prevHorPointerPosStatic).normalized;

					//This is a simplification of the Dot Product between 
					//the Mouse Direction Vector and the World Right Vector (normalized)
					val = deltaHorPointerPosStatic.x;
				} 

				prevHorPointerPosStatic = Input.mousePosition;
			} 
			else 
			{
				try 
				{
					if (Input.GetMouseButton (0) && !CanvasGraphicsRaycaster.Instance.IsPointerOverUI(ignoreUILayer, (Vector2) Input.mousePosition)) 
					{
						deltaHorPointerPosStatic = (realMousePos - prevHorPointerPosStatic).normalized;

						//This is a simplification of the Dot Product between 
						//the Mouse Direction Vector and the World Right Vector (normalized)
						val = deltaHorPointerPosStatic.x;
					} 

					prevHorPointerPosStatic = Input.mousePosition;
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
			Vector3 realMousePos = Input.mousePosition;
			float val = 0f;

			if (allowSwipeOverUI) 
			{
				if (Input.GetMouseButton (0)) 
				{
					deltaHorPointerPosRawStatic = realMousePos - prevHorPointerPosRawStatic;

					val = deltaHorPointerPosRawStatic.x;
				} 

				prevHorPointerPosRawStatic = Input.mousePosition;
			} 
			else 
			{
				try 
				{
					if (Input.GetMouseButton (0) && !CanvasGraphicsRaycaster.Instance.IsPointerOverUI(ignoreUILayer, (Vector2) Input.mousePosition)) 
					{
						deltaHorPointerPosRawStatic = realMousePos - prevHorPointerPosRawStatic;

						val = deltaHorPointerPosRawStatic.x;
					} 

					prevHorPointerPosRawStatic = Input.mousePosition;
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
			Vector3 realMousePos = Input.mousePosition;
			float val = 0f;

			if (allowSwipeOverUI) 
			{
				if (Input.GetMouseButton (0)) 
				{
					deltaVerPointerPosStatic = (realMousePos - prevVerPointerPosStatic).normalized;

					//This is a simplification of the Dot Product between 
					//the Mouse Direction Vector and the World Up Vector (normalized)
					val = deltaVerPointerPosStatic.y;
				} 

				prevVerPointerPosStatic = Input.mousePosition;
			} 
			else 
			{
				try 
				{
					if (Input.GetMouseButton (0) && !CanvasGraphicsRaycaster.Instance.IsPointerOverUI(ignoreUILayer, (Vector2) Input.mousePosition)) 
					{
						deltaVerPointerPosStatic = (realMousePos - prevVerPointerPosStatic).normalized;

						//This is a simplification of the Dot Product between 
						//the Mouse Direction Vector and the World Up Vector (normalized)
						val = deltaVerPointerPosStatic.y;
					} 

					prevVerPointerPosStatic = Input.mousePosition;
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
			Vector3 realMousePos = Input.mousePosition;
			float val = 0f;

			if (allowSwipeOverUI) 
			{
				if (Input.GetMouseButton (0)) 
				{
					deltaVerPointerPosRawStatic = realMousePos - prevVerPointerPosRawStatic;

					val = deltaVerPointerPosRawStatic.y;
				} 

				prevVerPointerPosRawStatic = Input.mousePosition;
			} 
			else 
			{
				try 
				{
					if (Input.GetMouseButton (0) && !CanvasGraphicsRaycaster.Instance.IsPointerOverUI(ignoreUILayer, (Vector2) Input.mousePosition)) 
					{
						deltaVerPointerPosRawStatic = realMousePos - prevVerPointerPosRawStatic;

						val = deltaVerPointerPosRawStatic.y;
					} 

					prevVerPointerPosRawStatic = Input.mousePosition;
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
			Rect rectArea = GetScreenCoord (rectTrans);

			Vector3 realMousePos = new Vector3 (Input.mousePosition.x, Screen.height - Input.mousePosition.y, Input.mousePosition.z);;
			float val = 0f;

			if (!rectArea.Contains ((Vector2)realMousePos))
				return 0f;

			if (allowSwipeOverUI)
			{
				if (Input.GetMouseButton (0))
				{
					deltaHorPointerRectPos = (Input.mousePosition - prevHorPointerRectPos).normalized;

					val = deltaHorPointerRectPos.x;
				} 

				prevHorPointerRectPos = Input.mousePosition;
			}
			else 
			{
				try
				{
					if (Input.GetMouseButton (0) && !CanvasGraphicsRaycaster.Instance.IsPointerOverUI(ignoreUILayer, (Vector2) Input.mousePosition))
					{
						deltaHorPointerRectPos = (Input.mousePosition - prevHorPointerRectPos).normalized;

						val = deltaHorPointerRectPos.x;
					} 

					prevHorPointerRectPos = Input.mousePosition;
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
			Rect rectArea = GetScreenCoord (rectTrans);

			Vector3 realMousePos = new Vector3 (Input.mousePosition.x, Screen.height - Input.mousePosition.y, Input.mousePosition.z);;
			float val = 0f;

			if (!rectArea.Contains ((Vector2)realMousePos))
				return 0f;

			if (allowSwipeOverUI)
			{
				if (Input.GetMouseButton (0))
				{
					deltaHorPointerRectRawPos = Input.mousePosition - prevHorPointerRectRawPos;

					val = deltaHorPointerRectRawPos.x;
				} 

				prevHorPointerRectRawPos = Input.mousePosition;
			} 
			else 
			{
				try 
				{
					if (Input.GetMouseButton (0) && !CanvasGraphicsRaycaster.Instance.IsPointerOverUI(ignoreUILayer, (Vector2) Input.mousePosition))
					{
						deltaHorPointerRectRawPos = Input.mousePosition - prevHorPointerRectRawPos;

						val = deltaHorPointerRectRawPos.x;
					} 

					prevHorPointerRectRawPos = Input.mousePosition;
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
			Rect rectArea = GetScreenCoord (rectTrans);

			Vector3 realMousePos = new Vector3 (Input.mousePosition.x, Screen.height - Input.mousePosition.y, Input.mousePosition.z);
			float val = 0f;

			if (!rectArea.Contains ((Vector2)realMousePos))
				return 0f;

			if (allowSwipeOverUI)
			{
				if (Input.GetMouseButton (0)) 
				{
					deltaVerPointerRectPos = (Input.mousePosition - prevVerPointerRectPos).normalized;

					val = deltaVerPointerRectPos.y;
				} 

				prevVerPointerRectPos = Input.mousePosition;
			}
			else 
			{
				try 
				{
					if (Input.GetMouseButton (0) && !CanvasGraphicsRaycaster.Instance.IsPointerOverUI(ignoreUILayer, (Vector2) Input.mousePosition)) 
					{
						deltaVerPointerRectPos = (Input.mousePosition - prevVerPointerRectPos).normalized;

						val = deltaVerPointerRectPos.y;
					} 

					prevVerPointerRectPos = Input.mousePosition;
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
			Rect rectArea = GetScreenCoord (rectTrans);

			Vector3 realMousePos = new Vector3 (Input.mousePosition.x, Screen.height - Input.mousePosition.y, Input.mousePosition.z);
			float val = 0f;

			if (!rectArea.Contains ((Vector2)realMousePos))
				return 0f;
				
			if (allowSwipeOverUI) 
			{
				if (Input.GetMouseButton (0)) 
				{
					deltaVerPointerRectRawPos = Input.mousePosition - prevVerPointerRectRawPos;

					val = deltaVerPointerRectRawPos.y;
				} 

				prevVerPointerRectRawPos = Input.mousePosition;
			} 
			else 
			{
				try 
				{
					if (Input.GetMouseButton (0) && !CanvasGraphicsRaycaster.Instance.IsPointerOverUI(ignoreUILayer, (Vector2) Input.mousePosition)) 
					{
						deltaVerPointerRectRawPos = Input.mousePosition - prevVerPointerRectRawPos;

						val = deltaVerPointerRectRawPos.y;
					} 

					prevVerPointerRectRawPos = Input.mousePosition;
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

			if (allowTouchOverUI)
			{
				if (Input.GetMouseButtonDown (0)) 
				{
					val = axisVal;
					isTouched = true;
				} 
				else if (Input.GetMouseButton (0)) 
				{
					val = axisVal;
					isTouched = true;
				} 
				else 
				{
					val = 0f;
					isTouched = false;
				}
			} 
			else 
			{
				try
				{
					if (Input.GetMouseButtonDown (0) && !CanvasGraphicsRaycaster.Instance.IsPointerOverUI(ignoreUILayer, (Vector2) Input.mousePosition)) 
					{
						val = axisVal;
						isTouched = true;
					} 
					else if (Input.GetMouseButton (0) && !CanvasGraphicsRaycaster.Instance.IsPointerOverUI(ignoreUILayer, (Vector2) Input.mousePosition)) 
					{
						val = axisVal;
						isTouched = true;
					} 
					else 
					{
						val = 0f;
						isTouched = false;
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
			Rect rectArea = GetScreenCoord (rectTrans);

			Vector3 realMousePos = new Vector3 (Input.mousePosition.x, Screen.height - Input.mousePosition.y, Input.mousePosition.z);
			float val = 0f;

			if (!rectArea.Contains ((Vector2)realMousePos))
				return 0f;

			if (allowTouchOverUI) 
			{
				if (Input.GetMouseButtonDown (0)) 
				{
					val = axisVal;
					isTouched = true;
				} 
				else if (Input.GetMouseButton (0)) 
				{
					val = axisVal;
					isTouched = true;
				} 
				else 
				{
					val = 0f;
					isTouched = false;
				}
			} 
			else 
			{
				try 
				{
					if (Input.GetMouseButtonDown (0) && !CanvasGraphicsRaycaster.Instance.IsPointerOverUI(ignoreUILayer, (Vector2) Input.mousePosition)) 
					{
						val = axisVal;
						isTouched = true;
					} 
					else if (Input.GetMouseButton (0) && !CanvasGraphicsRaycaster.Instance.IsPointerOverUI(ignoreUILayer, (Vector2) Input.mousePosition)) 
					{
						val = axisVal;
						isTouched = true;
					} 
					else 
					{
						val = 0f;
						isTouched = false;
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
