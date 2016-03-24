using UnityEngine;
using System.Collections;
using System;

namespace KamisNightmare.Controllers
{
    public class UIController : MonoBehaviour
    {
        public MenuController Menu;
        public UILabel CountdownLabel;
        public UILabel ScoreLabel;
        public UILabel HighScoreLabel;
        public GameObject SkipButton;

        internal float Score;
        internal float HighScore;
        internal bool KeepScore;

        private void Start()
        {
            if (null != HighScoreLabel)
            {
                if (PlayerPrefs.HasKey("HighScore"))
                {
                    HighScore = PlayerPrefs.GetFloat("HighScore");
                    HighScoreLabel.text = "HIGH SCORE: " + (int)HighScore;
                }
                else
                {
                    PlayerPrefs.SetFloat("HighScore", 0.0f);
                    HighScoreLabel.text = "HIGH SCORE: 0";
                }
            }
        }

        internal IEnumerator BeginCountdown(Action OnCountdownFinished)
        {
            Menu.Hide();
            yield return StartCoroutine(Countdown());
            KeepScore = true;
            OnCountdownFinished();
        }

        internal IEnumerator Countdown()
        {
            yield return new WaitForSeconds(0.5f);
            CountdownLabel.text = "GET READY!";
            CountdownLabel.enabled = true;
            yield return new WaitForSeconds(1.0f);
            CountdownLabel.text = "GO!";
            yield return new WaitForSeconds(0.5f);
            CountdownLabel.enabled = false;
        }

        internal void EnableSkipping()
        {
            if (null != SkipButton)
            {
                SkipButton.SetActive(true);
            }
        }

        internal void End()
        {
            KeepScore = false;
            Menu.Show();
            Score = 0.0f;
        }

        internal void ShowUnpause()
        {
            CountdownLabel.enabled = false;
        }

        internal void ShowPause()
        {
            CountdownLabel.text = "PAUSED";
            CountdownLabel.enabled = true;
        }

        private void Update()
        {
            if (KeepScore)
            {
                Score += Time.deltaTime;
                if (null != ScoreLabel)
                {
                    ScoreLabel.text = "SCORE: " + (int)Score;
                }

                if (Score > HighScore)
                {
                    HighScore = Score;
                    if (null != HighScoreLabel)
                    {
                        HighScoreLabel.text = "HIGH SCORE: " + (int)HighScore;
                    }
                    SaveScore();
                }
            }
        }

        private void SaveScore()
        {
            PlayerPrefs.SetFloat("HighScore", HighScore);
        }

        internal void OnSceneExit()
        {
            SaveScore();
        }

        private void OnApplicationQuit()
        {
            SaveScore();
        }
    }
}