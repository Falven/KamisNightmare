using UnityEngine;
using System.Collections;

namespace KamisNightmare.Controllers
{
	public class CutsceneKamiController : MonoBehaviour
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

		private void OnPanToPop()
		{
			if(null != _cutSceneController)
			{
				_cutSceneController.PanToPop();
			}
		}

		private void OnTeddyPopBalloons()
		{
			if(null != _cutSceneController)
			{
				_cutSceneController.TeddyPopBalloons();
			}
		}

		private void OnBalloonsPop()
		{
            if (null != _cutSceneController)
            {
                _cutSceneController.BalloonsPop();
            }
		}

		private void OnEndScene()
		{
			if(null != _cutSceneController)
			{
				_cutSceneController.EndScene();
			}
		}		
	}
}