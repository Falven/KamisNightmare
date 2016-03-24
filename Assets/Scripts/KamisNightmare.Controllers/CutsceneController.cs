using UnityEngine;
using System.Collections;

namespace KamisNightmare.Controllers
{
	public class CutsceneController : MonoBehaviour
	{
		public Camera Cam;
		public Transform CutsceneBalloons;
		public Transform CutsceneKami;
        public Transform CutscenePoppedBalloons;
		public Transform CutsceneTeddy;
		public Transform HorizontalInstructions;
		public Transform VerticalInstructions;
		public UIWidget SecretContainer;

		private BackgroundManager _bgManager;
		private UIController _uIController;
		private CameraController _camController;

		private void Awake()
		{
			_bgManager = BackgroundManager.Instance;
		}

		private void Start()
		{
			if(null == Cam)
			{
				Cam = Camera.main;
			}

			_camController = Cam.GetComponent<CameraController>();

			if(null == CutsceneKami)
			{
				CutsceneKami = GameObject.FindGameObjectWithTag("Player").transform;
			}

			_uIController = GetComponent<UIController>();
		}

		private void Update()
		{
			if(Input.GetKey(KeyCode.Escape))
			{
				Application.Quit();
			}
		}

		public void CutBalloons()
		{
			_camController.SmoothFollowTarget = CutsceneBalloons;
			_camController.SmoothFollowEnabled = true;
			CutsceneBalloons.animation.Play("Cutscene_Balloons_Fly");
			_uIController.EnableSkipping();
			SecretContainer.enabled = false;
		}

		public void CutToHorizontalInstructions()
		{
			_camController.SmoothFollowTarget = HorizontalInstructions;
			HorizontalInstructions.animation.Play();
		}

		public void CutToVerticalInstructions()
		{
			_camController.SmoothFollowTarget = VerticalInstructions;
			VerticalInstructions.animation.Play();
		}

		public void CutToKami()
		{
			_camController.SmoothFollowTarget = CutsceneKami;
			HorizontalInstructions.animation.Stop();
		}

		public void KamiJump()
		{
			CutsceneKami.GetComponent<Animator>().SetBool("BeginCutscene", true);
			VerticalInstructions.animation.Stop();
		}

		public void PanToPop()
		{
			_camController.dampTime = 0.2f;
		}

		public void CamZoomIn()
		{
			Cam.animation.Play("Camera_Zoom_In");
		}

		public void CamZoomOut()
		{
			Cam.animation.Play("Camera_Zoom_Out");
		}

		public void TeddyPopBalloons()
		{
			CutsceneTeddy.animation.Play("Teddy_Pop_Balloons");
		}

		public void BalloonsPop()
		{
            CutscenePoppedBalloons.animation.Play();
            CutscenePoppedBalloons.audio.Play();
			_bgManager.BeginScrolling();
		}

		public void EndScene()
		{
			Application.LoadLevel("GameScene");
		}
	}
}