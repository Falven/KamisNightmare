using UnityEngine;
using System.Collections;
using DapperApps.Plugins;

namespace KamisNightmare.Controllers
{
	public class GameController : MonoBehaviour
	{
		public Camera Cam;
		public PlayerController Player;

		internal BackgroundManager BackgroundManager;
		internal SpawnController SpawnManager;
		internal UIController UIManager;
		internal SoundController SoundManager;
		internal PauseController PauseManager;
		internal bool IsPaused = false;
		internal bool GameOver;

		private void Awake()
		{
			if(null == Cam)
			{
				Cam = Camera.main;
			}

			if(null == Player)
			{
				Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
			}

			BackgroundManager = BackgroundManager.Instance;
			SpawnManager = GetComponent<SpawnController>();
			UIManager = GetComponent<UIController>();
			SoundManager = GetComponent<SoundController>();
			PauseManager = GetComponent<PauseController>();
			GameOver = true;
		}

		private void Start()
		{
            UnityBridge.LoadAd();
			BackgroundManager.Reset();
			StartCoroutine(UIManager.BeginCountdown(FinishCountDown));
		}

		internal void Reset()
		{
            UnityBridge.LoadAd();
			SpawnManager.Reset();
			Player.Reset();
			BackgroundManager.Reset();
			SoundManager.Begin();
			StartCoroutine(UIManager.BeginCountdown(FinishCountDown));
		}

		private void FinishCountDown()
		{
			Player.Begin();
			SpawnManager.Begin();
			GameOver = false;
		}

		internal void End()
		{
            UnityBridge.ShowAd();
			GameOver = true;
			SoundManager.End();
			Player.End();
			UIManager.End();
			SpawnManager.End();
		}

		internal void Pause()
		{
			IsPaused = true;
			BackgroundManager.PauseScrolling();
			SoundManager.PauseSounds();
			UIManager.ShowPause();
			Time.timeScale = 0;
		}

		internal void Unpause()
		{
			IsPaused = false;
			BackgroundManager.UnpauseScrolling();
			SoundManager.UnpauseSounds();
			UIManager.ShowUnpause();
			Time.timeScale = 1;
		}
	}
}