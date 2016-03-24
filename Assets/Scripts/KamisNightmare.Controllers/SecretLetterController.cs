using UnityEngine;
using System.Collections;

namespace KamisNightmare.Controllers
{
	public class SecretLetterController : MonoBehaviour
	{
		private void OnPress(bool isPressed)
		{
			if(!isPressed)
			{
				GetComponentInParent<SecretController>().Text += this.name;
			}
		}
	}
}