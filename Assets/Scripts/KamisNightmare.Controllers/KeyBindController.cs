using UnityEngine;
using System.Collections;

namespace KamisNightmare.Controllers
{
    public class KeyBindController : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}