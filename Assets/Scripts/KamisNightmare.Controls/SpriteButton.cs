using UnityEngine;
using System;
using System.Collections;

namespace KamisNightmare.Controls
{
	public class SpriteButton : MonoBehaviour
	{
		public event EventHandler<EventArgs> Tap;

		public Camera Cam;
		private Bounds _buttonBounds;
		private bool _tapped;

		private void Start()
		{
			if(null == Cam)
			{
				Cam = Camera.main;
			}
			_buttonBounds = this.renderer.bounds;
			_tapped = false;
		}

		private void Update()
		{
			var mouse = Input.mousePresent;

			if(mouse || Input.touchCount == 1)
			{
				if(Input.GetMouseButtonDown(0) || Input.touchCount == 1)
				{
					Vector3 tapPos;
					if(mouse)
					{
						tapPos = Input.mousePosition;
					}
					else
					{
						var pos = Input.touches[0].position;
						tapPos = new Vector3(pos.x, pos.y);
					}
					tapPos = Cam.ScreenToWorldPoint(tapPos);
					tapPos.z = _buttonBounds.center.z;

					if(_buttonBounds.Contains(tapPos))
					{
						_tapped = true;
					}
				}
				else
				{
					if(_tapped)
					{
						RaiseTap();
						_tapped = false;
					}
				}
			}
		}

		private void RaiseTap()
		{
			if(null != Tap)
			{
				Tap(this, new EventArgs());
			}
		}
	}
}

