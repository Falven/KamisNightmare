using UnityEngine;
using System.Collections;

namespace KamisNightmare.Controls
{
	public class LoadingScreen : MonoBehaviour
	{
		public Texture2D Texture;
		internal static LoadingScreen instance;

		private void Awake()
		{
			if(instance)
			{
				Destroy(gameObject);
			}
			else
			{
				instance = this;
				gameObject.AddComponent<GUITexture>().enabled = false;
				guiTexture.texture = Texture;
				transform.position = new Vector3(0.5f, 0.5f, 0.0f);
				DontDestroyOnLoad(this);
			}
		}

		public static void Load(int index)
		{
			if(instance)
			{
				instance.guiTexture.enabled = true;
				Application.LoadLevel(index);
				instance.guiTexture.enabled = false;
			}
		}
	}
}