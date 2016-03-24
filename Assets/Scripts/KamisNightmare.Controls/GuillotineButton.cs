using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace KamisNightmare.Controls
{
	public class GuillotineButton : MonoBehaviour
	{
		public event EventHandler<EventArgs> TapRelease;
		public Camera Cam;
		public Transform Blade;
		
		private Bounds _buttonBounds;
		private float _minButtonX;
		private float _maxButtonX;
		private bool _tappedButton;
		private Vector3 _bladeOrigin;
		private Vector3 _bladePos;
		private Vector3 _tapOrigin;
		private List<ThresholdEvent> _thresholds;

		private void Start()
		{
			if(null == Cam)
			{
				Cam = Camera.main;
			}
			_bladeOrigin = Blade.position;
			_buttonBounds = this.renderer.bounds;
			_minButtonX = _buttonBounds.min.x;
			_maxButtonX = _buttonBounds.max.x;
			_tappedButton = false;
			_tapOrigin = Vector3.zero;
		}

		private void Update()
		{
			_bladePos = Blade.position;
			var mouse = Input.mousePresent;
			
			if((mouse && Input.GetMouseButton(0)) || Input.touchCount != 0)
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
				
				if(_tappedButton || _buttonBounds.Contains(tapPos))
				{
					_tappedButton = true;
					if(_tapOrigin == Vector3.zero)
					{
						_tapOrigin = tapPos;
					}
					
					var tapX = tapPos.x;
					var relTapX = Mathf.Abs(_buttonBounds.min.x) + tapX;
					var tapDelt = (tapPos.x - _tapOrigin.x);
					foreach(var threshevent in _thresholds)
					{
						if(tapDelt > _buttonBounds.extents.x && relTapX > threshevent.threshold)
						{
							threshevent.del(relTapX);
                            ResetPos();
						}
					}

					if(tapX > _minButtonX && tapX < _maxButtonX)
					{
						Vector3 newPos = new Vector3(tapX, _bladePos.y, _bladePos.z);
						Blade.Translate(newPos - _bladePos);
					}
				}
				else
				{
					_tapOrigin = Vector3.zero;
				}
			}
			else
			{
				if(_tappedButton)
				{
					_tappedButton = false;
					RaiseTapRelease();
                    ResetPos();
				}
			}
		}

		public void HookThresholdEvent(float offsetPercent, Action<float> del)
		{
			if(_thresholds == null)
			{
				_thresholds = new List<ThresholdEvent>();
			}

			if(offsetPercent > 1 || offsetPercent < 0 || del == null)
			{
				throw new ArgumentException();
			}

			_thresholds.Add(new ThresholdEvent() { threshold = (_buttonBounds.size.x * offsetPercent), del = del});
		}

		private void ResetPos()
		{
            _tapOrigin = Vector3.zero;
			Blade.Translate(_bladeOrigin - _bladePos);
		}

		private void RaiseTapRelease()
		{
			if(null != TapRelease)
			{
				TapRelease(this, new EventArgs());
			}
		}

		private struct ThresholdEvent
		{
			internal float threshold;
			internal Action<float> del;
		}
	}
}