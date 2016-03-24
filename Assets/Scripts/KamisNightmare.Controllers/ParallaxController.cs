using UnityEngine;
using System.Collections;

namespace KamisNightmare.Controllers
{
	public class ParallaxController : MonoBehaviour
	{
		public bool TweenColor = false;
		public float DeltaBackgroundColor = 0.00015f;
		public Vector2 TopSpeed = new Vector2(0f, 0f);

		internal bool Scroll = false;
		
		private float _time;
		private Color _originalColor;

		private void Start()
		{
			if(TweenColor && renderer.material.HasProperty("_Color"))
			{
				_originalColor = renderer.material.color;
			}
			else
			{
				TweenColor = false;
			}

			_time = 0.0f;
		}

		private void Update()
		{
			if(Scroll)
			{
				_time += Time.deltaTime;
				renderer.material.mainTextureOffset = new Vector2(TopSpeed.x, -(_time * TopSpeed.y));
			}

			if(TweenColor)
			{
				var curCol = renderer.material.color;
				renderer.material.color = new Color(curCol.r - DeltaBackgroundColor, curCol.g - DeltaBackgroundColor, curCol.b - DeltaBackgroundColor);
			}
		}

		internal void End()
		{
			Scroll = false;
			Reset();
		}

		internal void Reset()
		{
			_time = 0.0f;
			ResetColor();
		}

		internal void ResetColor()
		{
			if(TweenColor)
			{
				renderer.material.color = _originalColor;
			}
		}
	}
}