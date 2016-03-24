using UnityEngine;
using System;
using System.Collections;
using KamisNightmare.Controls;

namespace KamisNightmare.Controllers
{
	public class QuitController : MonoBehaviour
	{
		public AudioClip OpenClip;
		public AudioClip CloseClip;

		public HeadrestButton ButtonControl;
		private bool _quitting;
		private bool _tapped;

		private void Start()
		{
			_tapped = false;
			_quitting = false;
			if(null == ButtonControl)
			{
				ButtonControl = GetComponent<HeadrestButton>();
			}

			var source = (AudioSource)audio;
			ButtonControl.HookThresholdEvent(0.85f,
			(f) =>
			{
				if(!_quitting)
				{
					_quitting = true;
					Application.Quit();
				}
			});
			ButtonControl.TapPress +=
			(s, e) =>
			{
				if(!_tapped)
				{
					if(!source.isPlaying)
					{
						source.clip = OpenClip;
						source.Play();
					}
					_tapped = true;
				}
			};
			ButtonControl.TapRelease +=
			(s, e) =>
			{
				if(_tapped)
				{
					if(!source.isPlaying)
					{
						source.clip = CloseClip;
						source.Play();
					}
					_tapped = false;
				}
			};
		}
	}
}