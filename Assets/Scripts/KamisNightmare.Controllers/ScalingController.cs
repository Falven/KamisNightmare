using UnityEngine;
using System.Collections;

namespace KamisNightmare.Controllers
{
	public class ScalingController : MonoBehaviour
	{
		public Camera Cam;

		private void Awake()
		{
			if(null == Cam)
			{
				Cam = Camera.main;
			}

			float height = Cam.orthographicSize * 2.0f;
			float width = height * Screen.width / Screen.height;
			var scale = Mathf.Max(width, height);
			transform.localScale = new Vector3(scale, scale, transform.localScale.z);
		}
	}
}