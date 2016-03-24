using UnityEngine;
using System.Collections;

namespace KamisNightmare.Controllers
{
	public class TeleportationController : MonoBehaviour
	{
		public GameObject Player;
		public BoxCollider2D TopTeleporter;
		public BoxCollider2D LeftTeleporter;
		public BoxCollider2D BottomTeleporter;
		public BoxCollider2D RightTeleporter;

		private float _playerSize;

		private void Start()
		{
			if(null == Player)
			{
				Player = GameObject.FindGameObjectWithTag("Player");
			}
			_playerSize = Player.collider2D.bounds.size.y;
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if(other.tag == "Player")
			{
				var boxColl = this.collider2D as BoxCollider2D;
				if(null != boxColl)
				{
					var curPos = other.transform.position;
					Vector3 newPos;
					if(boxColl == TopTeleporter)
					{
						newPos = new Vector3(curPos.x, BottomTeleporter.center.y + _playerSize - (_playerSize * 0.2f), curPos.z);
					}
					else if(boxColl == LeftTeleporter)
					{
						newPos = new Vector3(RightTeleporter.center.x - _playerSize, curPos.y, curPos.z);
					}
					else if(boxColl == BottomTeleporter)
					{
						newPos = new Vector3(curPos.x, TopTeleporter.center.y - _playerSize + (_playerSize * 0.2f), curPos.z);
					}
					else
					{
						newPos = new Vector3(LeftTeleporter.center.x + _playerSize, curPos.y, curPos.z);
					}
					other.transform.position = newPos;
				}
			}
		}
	}
}