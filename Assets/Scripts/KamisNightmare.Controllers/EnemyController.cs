using UnityEngine;
using System.Collections;
using System;

namespace KamisNightmare.Controllers
{
	public class EnemyController : MonoBehaviour
	{
		internal bool Spawning = true;
		private RenderVisibilityReceiver _receiver;
		private static GameController _gameManager;
		private static SpawnController _spawnManager;

		private void Start()
		{
			if(null == _gameManager)
			{
				_gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
			}

			if(null == _spawnManager)
			{
				_spawnManager = _gameManager.GetComponent<SpawnController>();
			}

			_receiver = GetComponent<RenderVisibilityReceiver>();
			_receiver.ChildrenBecameVisible += OnChildrenBecameVisible;
			_receiver.ChildrenBecameInvisible += OnChildrenBecameInvisible;
		}

		private void OnCollisionEnter2D(Collision2D other)
		{
			if(other.transform.tag == "Player" && !_gameManager.GameOver)
			{
				_gameManager.End();
			}
		}

		private void OnChildrenBecameVisible(object sender, EventArgs e)
		{
			if(Spawning)
			{
				Spawning = false;
			}
		}

		private void OnChildrenBecameInvisible(object sender, EventArgs e)
		{
			if(!Spawning)
			{
				ToggleEnemySpecificControllers(false);
				_spawnManager.DeactivateMonster(this);
			}
		}

		private void ToggleEnemySpecificControllers(bool state)
		{
			var bunnyController = transform.parent.GetComponent<BunnyController>();
			var teddyController = transform.parent.GetComponent<TeddyController>();
			if(null != bunnyController)
			{
				bunnyController.enabled = state;
			}
			if(null != teddyController)
			{
				teddyController.enabled = state;
			}
		}

        internal void SetState(bool active)
        {
            ToggleEnemySpecificControllers(active);
            collider2D.enabled = active;
            Spawning = !active;
        }
	}
}