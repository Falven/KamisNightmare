using UnityEngine;
using System.Collections;

namespace KamisNightmare.Controllers
{
	public class FollowController : MonoBehaviour
	{
		public Transform Target;
		public float dampTime = 0.1f;
	
		private void Start()
		{
			var player = GameObject.FindGameObjectWithTag("Player");
			if(null != player)
			{
				Target = player.transform;
			}
		}

		private void Update()
		{
			if(null != Target)
			{
				var camera = Camera.main;
				var point = camera.WorldToViewportPoint(Target.position);
				var delta = Target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
				var destination = transform.position + delta;
				var vel = new Vector3(rigidbody2D.velocity.x, rigidbody2D.velocity.y, 0);
				transform.position = Vector3.SmoothDamp(transform.position, destination, ref vel, dampTime);
			}
		}
	}
}