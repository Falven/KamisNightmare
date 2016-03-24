using UnityEngine;
using System;
using System.Collections;
using KamisNightmare.Controls;

namespace KamisNightmare.Controllers
{
	public class FacebookButtonController : MonoBehaviour
	{
		public SpriteButton ButtonControl;

		private void Start()
		{
			if(null == ButtonControl)
			{
				ButtonControl = GetComponent<SpriteButton>();
			}
			ButtonControl.Tap += OnTap;
		}

		private void OnTap(object sender, EventArgs e)
		{
			Application.OpenURL("https://www.facebook.com/therealdapperapps");
		}		
	}
}