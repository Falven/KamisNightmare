using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace KamisNightmare.Controllers
{
	public class SoundController : MonoBehaviour
	{
        public AudioSource[] Sounds;
		public AudioClip GameMusic;
		public AudioClip GameOverMusic;

		private TweenVolume _volTween;
		private bool _isMuted;
		private int _muteState;

		private List<AudioSource> _pausedSounds;

		public bool IsMuted
		{
			get
			{
				return _isMuted;
			}
			set
			{
				_isMuted = value;
				_muteState = (_isMuted) ? 1 : 0;
				PlayerPrefs.SetInt("isMuted", _muteState);
				foreach(var sound in Sounds)
				{
					sound.mute = _isMuted;
				}
			}
		}

		public int MuteState
		{
			get
			{
				return _muteState;
			}
			set
			{
				_muteState = value;
				_isMuted = (_muteState != 0);
				PlayerPrefs.SetInt("isMuted", _muteState);
				foreach(var sound in Sounds)
				{
					sound.mute = _isMuted;
				}
			}
		}

		private void Awake()
		{
			_pausedSounds = new List<AudioSource>();
			if(PlayerPrefs.HasKey("isMuted"))
			{
				MuteState = PlayerPrefs.GetInt("isMuted");
			}
			else
			{
				PlayerPrefs.SetInt("isMuted", MuteState = 0);
			}
		}

		internal void End()
		{
			if(audio.isPlaying)
			{
				audio.Stop();
			}

			audio.clip = GameOverMusic;
			audio.Play();
		}

		internal void Begin()
		{
			if(audio.isPlaying)
			{
				audio.Stop();
			}
			
			audio.clip = GameMusic;
			audio.Play();
		}

		public void PauseSounds()
		{
			foreach(var sound in Sounds)
			{
				if(sound.isPlaying)
				{
					sound.Pause();
					_pausedSounds.Add(sound);
				}
			}
		}

		public void UnpauseSounds()
		{
			foreach(var sound in _pausedSounds)
			{
				sound.Play();
			}
			_pausedSounds.Clear();
		}

		private void OnApplicationQuit()
		{
			PlayerPrefs.SetInt("isMuted", MuteState);
		}
	}
}