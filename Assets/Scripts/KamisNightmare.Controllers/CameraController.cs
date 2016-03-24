using UnityEngine;
using System.Collections;

namespace KamisNightmare.Controllers
{
    public class CameraController : MonoBehaviour
    {
        public Camera Cam;
        public bool SmoothFollowEnabled = false;
        public Transform SmoothFollowTarget;
        public float dampTime = 0.75f;
        private Vector3 _velocity;

        private void Start()
        {
            _velocity = Vector3.zero;
            if(null == Cam)
            {
                Cam = Camera.main;
            }
        }

        private void Update()
        {
            if(SmoothFollowEnabled)
            {
                SmoothFollow();
            }
        }

        private void SmoothFollow()
        {
            if(null != SmoothFollowTarget)
            {
                var followPos = SmoothFollowTarget.position;
                var point = Cam.WorldToViewportPoint(followPos);
                var delta = followPos - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
                var destination = transform.position + delta;
                transform.position = Vector3.SmoothDamp(transform.position, destination, ref _velocity, dampTime);
            }
        }
    }
}