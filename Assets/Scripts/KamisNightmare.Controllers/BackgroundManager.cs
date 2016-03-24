namespace KamisNightmare.Controllers
{
	using UnityEngine;
	using System.Collections;
	using KamisNightmare.Controls;

	public class BackgroundManager : Singleton<BackgroundManager>
	{
		public ParallaxController ForegroundController;
		public ParallaxController MidgroundController;
		public ParallaxController BackgroundController;

		protected BackgroundManager()
		{
		}

		internal void BeginScrolling()
		{
			BackgroundController.Scroll = true;
			MidgroundController.Scroll = true;
			ForegroundController.gameObject.SetActive(true);
			ForegroundController.Scroll = true;
		}

		internal void EndScrolling()
		{
			BackgroundController.End();
			MidgroundController.End();
			ForegroundController.End();
			ForegroundController.gameObject.SetActive(false);
		}

		internal void PauseScrolling()
		{
			BackgroundController.Scroll = false;
			BackgroundController.TweenColor = false;
			MidgroundController.Scroll = false;
			MidgroundController.TweenColor = false;
			ForegroundController.Scroll = false;
		}

		internal void UnpauseScrolling()
		{
			BackgroundController.Scroll = true;
			BackgroundController.TweenColor = true;
			MidgroundController.Scroll = true;
			MidgroundController.TweenColor = true;
			ForegroundController.Scroll = true;
		}

		internal void Reset()
		{
			BackgroundController.Reset();
			MidgroundController.Reset();
			ForegroundController.Reset();
			BeginScrolling();
		}

		internal void ResetColor()
		{
			BackgroundController.ResetColor();
			MidgroundController.ResetColor();
		}
	}
}