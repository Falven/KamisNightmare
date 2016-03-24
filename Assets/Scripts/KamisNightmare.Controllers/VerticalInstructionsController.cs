using UnityEngine;
using System.Collections;

namespace KamisNightmare.Controllers
{
	public class VerticalInstructionsController : MonoBehaviour
	{
		private void PlaySchwoop()
		{
			if(!audio.isPlaying)
			{
				audio.Play();
			}
		}
	}
}