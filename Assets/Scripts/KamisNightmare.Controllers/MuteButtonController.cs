using UnityEngine;
using System;
using System.Collections;
using KamisNightmare.Controls;

namespace KamisNightmare.Controllers
{
	public class MuteButtonController : MonoBehaviour
	{
		public SpriteButton ButtonControl;
		public SoundController MuteController;
		public Sprite Unmuted;
		public Sprite Muted;
		private SpriteRenderer _renderer;
		
		private void Start()
		{
			if(null == ButtonControl)
			{
				ButtonControl = GetComponent<SpriteButton>();
			}
			ButtonControl.Tap += OnTap;

			if(null == MuteController)
			{
				MuteController = GameObject.FindGameObjectWithTag("GameController").GetComponent<SoundController>();
			}

			_renderer = (SpriteRenderer)renderer;
			_renderer.sprite = MuteController.IsMuted ? Muted : Unmuted;
		}

		private void OnTap(object sender, EventArgs e)
		{
			var muted = (MuteController.IsMuted = !MuteController.IsMuted);
			_renderer.sprite = muted ? Muted : Unmuted;
		}
	}
}