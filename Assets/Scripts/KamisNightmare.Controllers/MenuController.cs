using System;
using System.Collections;
using UnityEngine;

namespace KamisNightmare.Controllers
{
	public class MenuController : MonoBehaviour
	{
		internal void Show()
		{
			if(!gameObject.activeSelf)
			{
				gameObject.SetActive(true);
				animation.Play("GameOverMenu_FadeIn");
			}
		}

		internal void Hide()
		{
			if(gameObject.activeSelf)
			{
                animation.Play("GameOverMenu_FadeOut");
			}
		}

        private void OnHideFinished()
        {
            gameObject.SetActive(false);
        }
	}
}