using UnityEngine;
using System.Collections;

public class BunnyController : MonoBehaviour
{
	public Transform Target;
	
	private void OnEnable()
	{
		if(null == Target)
		{
			Target = GameObject.FindGameObjectWithTag("Player").transform;
		}
		ForceTrajectory();
	}
	
	private void ForceTrajectory()
	{
		var pos = transform.position;
		var tarPos = Target.transform.position;
		var angle = ((tarPos - pos) / 3);
		rigidbody2D.AddForce(angle, ForceMode2D.Impulse);
	}
}
