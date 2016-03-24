using UnityEngine;
using System.Collections;

namespace KamisNightmare.Controllers
{
	public class TeddyController : MonoBehaviour
	{
		public Transform Target;
		public float ForceDelay = 3.0f;

		private void OnEnable()
		{
			if(null == Target)
			{
				Target = GameObject.FindGameObjectWithTag("Player").transform;
			}
			Invoke("ForceTrajectory", ForceDelay);
		}
    
		private void ForceTrajectory()
		{
			if(this.enabled)
			{
				var pos = transform.position;
				var tarPos = Target.transform.position;
				var angle = tarPos - pos;
				var force = new Vector2(angle.x / 2.0f, angle.y / 2.0f);
				rigidbody2D.AddForce(force, ForceMode2D.Impulse);
				Invoke("ForceTrajectory", ForceDelay);
			}
		}
	}
}