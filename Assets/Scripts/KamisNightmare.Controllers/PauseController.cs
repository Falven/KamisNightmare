using System;
using System.Collections;
using UnityEngine;

namespace KamisNightmare.Controllers
{
	public class PauseController : MonoBehaviour
	{
		public GameController GameManager;

		private void Start()
		{
			if(null == GameManager)
			{
				GameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
			}
		}

		private void OnPress(bool isPressed)
		{
            if (!isPressed && !GameManager.GameOver)
            {
                if (GameManager.IsPaused)
                {
                    GameManager.Unpause();
                }
                else
                {
                    GameManager.Pause();
                }
            }
		}

#if !UNITY_EDITOR
        // Called when phone sends app to background.
        private void OnApplicationPause(bool paused)
        {

            if (!GameManager.IsPaused && !GameManager.GameOver)
            {
                GameManager.Pause();
            }
        }
#endif
    }
}