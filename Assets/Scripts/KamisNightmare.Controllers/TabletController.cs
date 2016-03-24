using UnityEngine;
using System.Collections;

namespace KamisNightmare.Controllers
{
	public class TabletController : MonoBehaviour
	{
		public int TabletDifficulty = 3;

		private SpawnController _spawnManager;

		private void Awake()
		{
            _spawnManager = GetComponent<SpawnController>();
			if(Screen.width > Screen.height)
			{
				_spawnManager.MaxMonsters *= TabletDifficulty;
				_spawnManager.MaxSpawnSpeed *= (float)TabletDifficulty;
			}
		}
	}
}