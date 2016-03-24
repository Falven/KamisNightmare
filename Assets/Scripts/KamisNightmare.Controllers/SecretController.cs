using UnityEngine;
using System.Collections;

namespace KamisNightmare.Controllers
{
	public class SecretController : MonoBehaviour
	{
		private string _text = "";
		public string Text
		{
			get
			{
				return _text;
			}
			set
			{
				_text = value;
				if(_text.Length == 3)
				{
					if(_text == "RAT")
					{
						GameObject.Find("Title").audio.Play();
					}
					_text = "";
				}
			}
		}		
	}
}