using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace KamisNightmare.Controllers
{
	public class SpawnController : MonoBehaviour
	{
		public float HighScore = 15.0f;
		public float MaxSpawnSpeed = 2.0f;
		public int MaxMonsters = 6;
		public GameObject[] MonsterOptions;
		public BoxCollider2D LeftSpawn;
		public BoxCollider2D RightSpawn;

		internal IList<GameObject> ActiveMonsters;
		internal IList<GameObject> LeftMonsters;
		internal IList<GameObject> RightMonsters;

		private UIController _uIManager;
		private float _curScore;
		private float _spawnSpeed;
		private int _spawnMax;
		private bool _stop;

		internal void Start()
		{
			_uIManager = GetComponent<UIController>();

			ActiveMonsters = new List<GameObject>(MaxMonsters);
			var maxMonstersPerSpawn = MaxMonsters / 2;
			LeftMonsters = new List<GameObject>(maxMonstersPerSpawn);
			RightMonsters = new List<GameObject>(maxMonstersPerSpawn);

			if(maxMonstersPerSpawn < MonsterOptions.Length)
			{
				SpawnMonsters(LeftSpawn, LeftMonsters, maxMonstersPerSpawn, 0);
				SpawnMonsters(LeftSpawn, LeftMonsters, maxMonstersPerSpawn, maxMonstersPerSpawn);
			}
			else
			{
				SpawnMonsters(LeftSpawn, LeftMonsters, maxMonstersPerSpawn, 0);
				SpawnMonsters(RightSpawn, RightMonsters, maxMonstersPerSpawn, 0);
			}

			_spawnMax = 0;
			_stop = true;
		}

		private void SpawnMonsters(BoxCollider2D spawn, IList<GameObject> monsterList, int number, int option)
		{
			var optionLength = MonsterOptions.Length;
			for(var i = 0; i < number; i++)
			{
                var monster = (GameObject)Instantiate(MonsterOptions[option], GetFreeSpawnPoint(spawn, monsterList), Quaternion.identity);
                monster.GetComponentInChildren<EnemyController>().SetState(false);
                monsterList.Add(monster);
				if(option++ >= optionLength)
					option = 0;
			}
		}

		internal Vector3 GetFreeSpawnPoint(BoxCollider2D spawn, IList<GameObject> spawned)
		{
			var center = spawn.center;
			var size = spawn.size;
			Vector3 spawnPoint;
			bool spawnPointTaken;
			do
			{
				spawnPointTaken = false;
				spawnPoint = new Vector3(center.x, Random.Range(center.y - (size.y / 2.0f), center.y + (size.y / 2.0f)));
				foreach(var monster in spawned)
				{
					if(monster.GetComponentInChildren<Collider2D>().bounds.Contains(spawnPoint))
					{
						spawnPointTaken = true;
						break;
					}
				}
			} while (spawnPointTaken);
			return spawnPoint;
		}

		internal void Begin()
		{
			_stop = false;
		}

		internal void End()
		{
			_stop = true;
		}

		internal void Reset()
		{
			if(null != ActiveMonsters)
			{
				while(ActiveMonsters.Count > 0)
				{
					DeactivateMonster(ActiveMonsters[0].GetComponentInChildren<EnemyController>());
				}
			}
		}

		private void Update()
		{
			if(!_stop)
			{
				_curScore = _uIManager.Score;

				_spawnMax = Mathf.Min((int)(Mathf.Floor(Mathf.Log(_curScore + 1.0f, 1.5f) * 0.35f) + 3), MaxMonsters);
				_spawnSpeed = Mathf.Min(Mathf.Floor(((MaxSpawnSpeed / HighScore) * _curScore) + 1.0f), MaxSpawnSpeed);

				if(ActiveMonsters.Count < _spawnMax)
				{
					var spawn = Random.Range(0, 2);
					if(spawn == 0)
					{
						if(LeftMonsters.Count > 0)
						{
							ActivateMonster(LeftMonsters, _spawnSpeed);
						}
					}
					else
					{
						if(RightMonsters.Count > 0)
						{
							ActivateMonster(RightMonsters, -_spawnSpeed);
						}
					}
				}
			}
		}

		private void ActivateMonster(IList<GameObject> monsterList, float forceX)
		{
			var monsterIndex = Random.Range(0, monsterList.Count);
			var monster = monsterList[monsterIndex];
			monsterList.RemoveAt(monsterIndex);
			ActiveMonsters.Add(monster);
			var forceY = Random.Range(-(forceX / 2.0f), forceX / 2.0f);
			monster.transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(forceX, forceY), ForceMode2D.Impulse);
            monster.GetComponentInChildren<EnemyController>().SetState(true);
		}

		internal void DeactivateMonster(EnemyController monster)
		{
			var containerTransform = monster.collider2D.transform.parent;
			var containerGameobject = containerTransform.gameObject;
			var maxMonstersPerSpawn = MaxMonsters / 2;
			Vector3 spawnPoint;
			var spawn = UnityEngine.Random.Range(0, 2);
			if(spawn == 0 && LeftMonsters.Count < maxMonstersPerSpawn)
			{
				LeftMonsters.Add(containerGameobject);
				spawnPoint = GetFreeSpawnPoint(LeftSpawn, LeftMonsters);
			}
			else
			{
				RightMonsters.Add(containerGameobject);
				spawnPoint = GetFreeSpawnPoint(RightSpawn, RightMonsters);
			}
            monster.GetComponentInChildren<EnemyController>().SetState(false);
			containerTransform.rigidbody2D.velocity = Vector2.zero;
			containerTransform.position = spawnPoint;
			ActiveMonsters.Remove(containerGameobject);
		}
	}
}