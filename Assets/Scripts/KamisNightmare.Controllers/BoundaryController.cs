using UnityEngine;
using System.Collections;

namespace KamisNightmare.Controllers
{
	public class BoundaryController : MonoBehaviour
	{
		public BoxCollider2D Top;
		public float Top_Width = 1.0f;
		public float Top_Height = 1.0f;
		public float Top_Padding_Top = 0.0f;
		public float Top_Padding_Left = 0.0f;

		public BoxCollider2D Left;
		public float Left_Width = 1.0f;
		public float Left_Height = 1.0f;
		public float Left_Padding_Top = 0.0f;
		public float Left_Padding_Left = 0.0f;
		
		public BoxCollider2D Bottom;
		public float Bottom_Width = 1.0f;
		public float Bottom_Height = 1.0f;
		public float Bottom_Padding_Top = 0.0f;
		public float Bottom_Padding_Left = 0.0f;
		
		public BoxCollider2D Right;
		public float Right_Width = 1.0f;
		public float Right_Height = 1.0f;
		public float Right_Padding_Top = 0.0f;
		public float Right_Padding_Left = 0.0f;

		private void Awake()
		{
			RefreshBoundaries();
		}

		internal void RefreshBoundaries()
		{
			var cam = Camera.main;
			var camPos = cam.transform.position;
			var screenDimensions = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
			var width = screenDimensions.x * 2.0f;
			var height = screenDimensions.y * 2.0f;
			var halfWidth = width / 2.0f;
			var halfHeight = height / 2.0f;
			
			if(null != Top)
			{
				Top.size = new Vector2(width + (Top_Width * 2.0f), Top_Height);
				Top.center = new Vector2(camPos.x + Top_Padding_Left, camPos.y + halfHeight + (Top_Height / 2.0f) - Top_Padding_Top);
			}
			
			if(null != Left)
			{
				Left.size = new Vector2(Left_Width, height + (Left_Height * 2.0f));
				Left.center = new Vector2(camPos.x - halfWidth - (Left_Width / 2.0f) + Left_Padding_Left, camPos.y - Left_Padding_Top);
			}
			
			if(null != Bottom)
			{
				Bottom.size = new Vector2(width + (Bottom_Width * 2.0f), Bottom_Height);
				Bottom.center = new Vector2(camPos.x + Bottom_Padding_Left, camPos.y - halfHeight - (Bottom_Height / 2.0f) - Bottom_Padding_Top);
			}
			
			if(null != Right)
			{
				Right.size = new Vector2(Right_Width, height + (Right_Height * 2.0f));
				Right.center = new Vector2(camPos.x + halfWidth + (Right_Width / 2.0f) + Right_Padding_Left, camPos.y - Right_Padding_Top);
			}
		}
	}
}