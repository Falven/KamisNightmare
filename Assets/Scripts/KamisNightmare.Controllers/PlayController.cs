using UnityEngine;
using System;
using System.Collections;
using KamisNightmare.Controls;

namespace KamisNightmare.Controllers
{
	public class PlayController : MonoBehaviour
	{
		public GameObject CutsceneManager;
		public GuillotineButton ButtonControl;
		
		private CutsceneController _cutSceneController;
		private bool _playing;

		private void Start()
		{
			if(null == CutsceneManager)
			{
				CutsceneManager = GameObject.FindGameObjectWithTag("GameController");
			}
			_cutSceneController = CutsceneManager.GetComponent<CutsceneController>();

			_playing = false;
			if(null == ButtonControl)
			{
				ButtonControl = GetComponent<GuillotineButton>();
			}

			ButtonControl.HookThresholdEvent(0.75f, OnSlice);
		}

		private void OnSlice(float percent)
		{
			if(!_playing)
			{
				_playing = true;
				if(!audio.isPlaying)
				{
					audio.Play();
				}
				animation.Play();
			}
		}

		private void OnCutBalloons()
		{
			_cutSceneController.CutBalloons();
            this.enabled = false;
		}
	}
}