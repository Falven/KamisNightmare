using UnityEngine;
using System.Collections;

namespace KamisNightmare.Controllers
{
	public class PlayerController : MonoBehaviour
	{
		public AudioClip Hit;
		public AudioClip Schwoop;

		public float HorizontalVelocity = 19.0f;
		public float UpVelocity = 6.0f;
		public float DownVelocity = 3.0f;
	
		private Animator _animator;
		private bool _touch;
		private bool _stop;

		internal void Start()
		{
			if(null == _animator)
				_animator = GetComponent<Animator>();
			_stop = true;
		}

		internal void Reset()
		{
            if(null != _animator)
			    _animator.SetBool("GameOver", false);
            
            if (null != rigidbody2D)
            {
                rigidbody2D.velocity = Vector2.zero;
                rigidbody2D.fixedAngle = true;
            }

            if (null != transform)
            {
                transform.position = Vector3.zero;
                transform.rotation = Quaternion.identity;
            }
		}

		internal void Begin()
		{
			_stop = false;
		}

		internal void End()
		{
			_stop = true;
			
            PlayHit();

            if(null != _animator)
			    _animator.SetBool("GameOver", true);

            if (null != rigidbody2D)
            {
                rigidbody2D.fixedAngle = false;
                rigidbody2D.AddTorque(Random.Range(-2.0f, 2.0f), ForceMode2D.Impulse);
            }
		}

		private void FixedUpdate()
		{
			if(!_stop)
			{
				var xSpeed = Input.acceleration.x * HorizontalVelocity;
				
				float ySpeed;
				if(Input.touchCount > 0)
				{
					if(!_touch)
						PlaySchwoop();

					_touch = true;
					ySpeed = UpVelocity;
				}
				else
				{
					_touch = false;
					ySpeed = -DownVelocity;
				}
				
                if(null != _animator)

                    if (null != _animator)
                    {
                        _animator.SetFloat("VelocityX", Mathf.Abs(xSpeed));
                        _animator.SetFloat("VelocityY", ySpeed); 
                    }
				
                if(null != rigidbody2D)
				    rigidbody2D.velocity = new Vector2(xSpeed, ySpeed);
			}
		}

		private void PlaySchwoop()
		{
			if(null != audio && null != Schwoop && !audio.isPlaying)
			{
				audio.clip = Schwoop;
				audio.Play();
			}
		}

		private void PlayHit()
		{
            if (null != audio && null != Hit && !audio.isPlaying)
            {
                audio.clip = Hit;
                audio.Play();
            }
		}
	}
}