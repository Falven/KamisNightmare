using UnityEngine;
using System;
using System.Collections;
using KamisNightmare.Controls;

namespace KamisNightmare.Controllers
{
	public class SkipController : MonoBehaviour
	{
        private bool _loading = false;

        private void OnPress(bool isPressed)
        {
            if (!isPressed && !_loading)
            {
                _loading = true;
                Application.LoadLevel("GameScene");
            }
        }
	}
}