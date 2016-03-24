using System;

namespace KamisNightmare.Controls
{
	using UnityEngine;
	
	/// <summary>
	/// Be aware this will not prevent a non singleton constructor
	///	such as "T myT = new T();"
	/// To prevent that, add `protected T () {}` to your singleton class.
	/// 
	/// As a note, this is made as MonoBehaviour because we need Coroutines.
	/// </summary>
	public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T _instance;
		
		private static object _lock = new object();
		
		public static T Instance
		{
			get
			{
				if(applicationIsQuitting)
				{
					return null;
				}
				
				lock(_lock)
				{
					if(_instance == null)
					{
						// Try to find object.
						_instance = (T)FindObjectOfType(typeof(T));

						// If not found,
						if(_instance == null)
						{
							// Try to create from prefabs.
							_instance = (T)Instantiate(Resources.Load(typeof(T).Name, typeof(T)));

							// If not a prefab.
							if(null == _instance)
							{
								//Create empty instance with component.
								GameObject singleton = new GameObject();
								_instance = singleton.AddComponent<T>();
								singleton.name = typeof(T).Name;
							}

							DontDestroyOnLoad(_instance.gameObject);
						}
					}
					return _instance;
				}
			}
		}
		
		private static bool applicationIsQuitting = false;

		/// <summary>
		/// When Unity quits, it destroys objects in a random order.
		/// In principle, a Singleton is only destroyed when application quits.
		/// If any script calls Instance after it have been destroyed, 
		///   it will create a buggy ghost object that will stay on the Editor scene
		///   even after stopping playing the Application. Really bad!
		/// So, this was made to be sure we're not creating that buggy ghost object.
		/// </summary>
		public void OnDestroy()
		{
			applicationIsQuitting = true;
		}
	}
}

