using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace KamisNightmare.Controls
{
	public class HeadrestButton : MonoBehaviour
	{
		public event EventHandler<EventArgs> TapRelease;
		public event EventHandler<EventArgs> TapPress;
		public Camera Cam;
		public Transform TopPlank;
		public Transform BottomPlank;

		private Bounds _buttonBounds;
		private float _buttonMinX;
		private float _buttonMaxX;

		private Vector3 _TPOrigin;
		private Vector3 _BPOrigin;
		private float _TPDeltY;
		private float _BPDeltY;

		private bool _tappedButton;
		private Vector3 _tapOrigin;
		private List<ThresholdEvent> _thresholds;

		private void Start()
		{	
			if(null == _thresholds)
			{
				_thresholds = new List<ThresholdEvent>();
			}

			if(null == Cam)
			{
				Cam = Camera.main;
			}
			
			_buttonBounds = this.renderer.bounds;
			_buttonMinX = _buttonBounds.min.x;
			_buttonMaxX = _buttonBounds.max.x;

			_TPOrigin = TopPlank.position;
			_BPOrigin = BottomPlank.position;
			_TPDeltY = TopPlank.renderer.bounds.extents.y / (_buttonBounds.size.x - _buttonBounds.extents.x / 2.0f);
			_BPDeltY = BottomPlank.renderer.bounds.extents.y / (_buttonBounds.size.x - _buttonBounds.extents.x / 2.0f);
			
			_tappedButton = false;
			_tapOrigin = Vector3.zero;
		}

		private void Update()
		{
			var curTPPos = TopPlank.position;
			var curBPPos = BottomPlank.position;

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
					RaiseTapPress();

					if(_tapOrigin == Vector3.zero)
					{
						_tapOrigin = tapPos;
					}

					var tapX = tapPos.x;
					var relTapX = Mathf.Abs(_buttonBounds.min.x) + tapX;

					foreach(var threshevent in _thresholds)
					{
						if(((tapPos.x - _tapOrigin.x) > _buttonBounds.extents.x) && relTapX > threshevent.threshold)
						{
							threshevent.del(relTapX);
						}
					}

					if(tapX > _buttonMinX && tapX < _buttonMaxX)
					{
						var tpTrans = _TPOrigin.y + (relTapX * _TPDeltY);
						var bpTrans = _BPOrigin.y - (relTapX * _BPDeltY);
						TopPlank.Translate(new Vector3(curTPPos.x, tpTrans, curTPPos.z) - curTPPos);
						BottomPlank.Translate(new Vector3(curBPPos.x, bpTrans, curBPPos.z) - curBPPos);
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
					TopPlank.Translate(_TPOrigin - curTPPos);
					BottomPlank.Translate(_BPOrigin - curBPPos);
					_tappedButton = false;
					_tapOrigin = Vector3.zero;
					RaiseTapRelease();
				}
			}
		}

		public void HookThresholdEvent(float offsetPercent, Action<float> del)
		{
			if(null == _thresholds)
			{
				_thresholds = new List<ThresholdEvent>();
			}

			if(offsetPercent > 1 || offsetPercent < 0 || del == null)
			{
				throw new ArgumentException();
			}
			_thresholds.Add(new ThresholdEvent() { threshold = (_buttonBounds.size.x * offsetPercent), del = del});
		}

		private void RaiseTapPress()
		{
			if(null != TapPress)
			{
				TapPress(this, new EventArgs());
			}
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