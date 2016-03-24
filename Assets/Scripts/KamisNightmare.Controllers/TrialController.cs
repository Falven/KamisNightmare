using UnityEngine;
using System.Collections;

namespace KamisNightmare.Controllers
{
    public class TrialController : MonoBehaviour
    {
        public Camera Cam;
        public static bool IsTrialMode = false;
        public BoundaryController Boundaries;
        public BoundaryController Recyclers;
        public BoundaryController Teleporters;

        private bool _updated;

        private void Start()
        {
            if (null == Cam)
            {
                Cam = Camera.main;
            }
            _updated = false;
        }

        private void Update()
        {
            if (IsTrialMode && !_updated)
            {
                if (null != Boundaries)
                {
                    Boundaries.Bottom_Padding_Top -= 1f;
                    Boundaries.RefreshBoundaries();
                }
                if (null != Recyclers)
                {
                    Recyclers.Bottom_Padding_Top -= 1f;
                    Recyclers.RefreshBoundaries();
                }
                if (null != Teleporters)
                {
                    Teleporters.Bottom_Padding_Top -= 1f;
                    Teleporters.RefreshBoundaries();
                }
                _updated = true;
            }
        }
    }
}