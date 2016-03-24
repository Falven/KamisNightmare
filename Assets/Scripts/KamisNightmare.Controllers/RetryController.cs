using UnityEngine;
using System;
using System.Collections;
using KamisNightmare.Controls;

namespace KamisNightmare.Controllers
{
	public class RetryController : MonoBehaviour
	{
		public GameController GameController;
		public GuillotineButton ButtonControl;

		private bool _retrying;

		internal void Start()
		{
			if(null == GameController)
			{
				GameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
			}

			if(null == ButtonControl)
			{
				ButtonControl = GetComponent<GuillotineButton>();
			}

			ButtonControl.HookThresholdEvent(0.75f, OnSlice);
		}

		private void OnSlice(float percent)
		{
			if(GameController.GameOver)
			{
				audio.Play();
				animation.Play();
                GameController.Reset();
			}
		}

		private void Retry()
		{
            //GameController.Reset();
		}
	}
}
