using UnityEngine;
using System.Collections;

namespace KamisNightmare.Controllers
{
	public class CutsceneBalloonController : MonoBehaviour
	{
		public GameObject CutsceneManager;

		private CutsceneController _cutSceneController;

		private void Start()
		{
			if(null == CutsceneManager)
			{
				CutsceneManager = GameObject.FindGameObjectWithTag("GameController");
			}
			_cutSceneController = CutsceneManager.GetComponent<CutsceneController>();
		}

		private void OnCutToHorizontalInstructions()
		{
			if(null != _cutSceneController)
			{
				_cutSceneController.CutToHorizontalInstructions();
			}
		}

		private void OnCamZoomIn()
		{
			if(null != _cutSceneController)
			{
				_cutSceneController.CamZoomIn();
			}
		}

		private void OnCamZoomOut()
		{
			if(null != _cutSceneController)
			{
				_cutSceneController.CamZoomOut();
			}
		}

		private void OnCutToVerticalInstructions()
		{
			if(null != _cutSceneController)
			{
				_cutSceneController.CutToVerticalInstructions();
			}
		}

		private void OnCutToKami()
		{
			if(null != _cutSceneController)
			{
				_cutSceneController.CutToKami();
			}
		}

		private void OnKamiJump()
		{
			if(null != _cutSceneController)
			{
				_cutSceneController.KamiJump();
			}
		}
	}
}