using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KamisNightmare.Controllers
{
    public class RecycleController : MonoBehaviour
    {
        public BoxCollider2D LeftSpawn;
        public BoxCollider2D RightSpawn;
        public SpawnController SpawnController;

        internal void Reset()
        {
            if (null != SpawnController)
            {
                var activeMonsters = SpawnController.ActiveMonsters;

                if (null != activeMonsters)
                {
                    while (activeMonsters.Count > 0)
                    {
                        var monster = activeMonsters[0];
                        ResetMonster(monster.GetComponentInChildren<Collider2D>());
                    }
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Enemy")
            {
                var controller = other.GetComponent<EnemyController>();
                if (null != controller && !controller.Spawning)
                {
                    ResetMonster(other);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag == "Enemy")
            {
                var controller = other.GetComponent<EnemyController>();
                if (null != controller && controller.Spawning)
                {
                    controller.Spawning = false;
                }
            }
        }

        private void ResetMonster(Collider2D monster)
        {
            var monsterCollider = monster;
            var monsterController = monster.GetComponent<EnemyController>();
            var containerTransform = monsterCollider.transform.parent;
            var containerGameobject = containerTransform.gameObject;

            var maxMonstersPerSpawn = SpawnController.MaxMonsters / 2;
            Vector3 spawnPoint;
            var spawn = UnityEngine.Random.Range(0, 2);
            if (spawn == 0 && SpawnController.LeftMonsters.Count < maxMonstersPerSpawn)
            {
                SpawnController.LeftMonsters.Add(containerGameobject);
                spawnPoint = SpawnController.GetFreeSpawnPoint(LeftSpawn, SpawnController.LeftMonsters);
            }
            else
            {
                SpawnController.RightMonsters.Add(containerGameobject);
                spawnPoint = SpawnController.GetFreeSpawnPoint(RightSpawn, SpawnController.RightMonsters);
            }
            DisableEnemySpecificControllers(containerGameobject);
            monsterCollider.enabled = false;
            monsterController.Spawning = true;
            containerTransform.rigidbody2D.velocity = Vector2.zero;
            containerTransform.position = spawnPoint;
            SpawnController.ActiveMonsters.Remove(containerGameobject);
        }

        private void DisableEnemySpecificControllers(GameObject monster)
        {
            var bunnyController = monster.GetComponent<BunnyController>();
            var teddyController = monster.GetComponent<TeddyController>();
            if (null != bunnyController)
            {
                bunnyController.enabled = false;
            }
            if (null != teddyController)
            {
                teddyController.enabled = false;
            }
        }
    }
}