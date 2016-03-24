using UnityEngine;
using System.Collections;
using System;

namespace KamisNightmare.Controllers
{
    public class RenderVisibilityReceiver : MonoBehaviour
    {
        public event EventHandler<EventArgs> ChildrenBecameVisible;
        public event EventHandler<EventArgs> ChildrenBecameInvisible;

        internal int TotalRenders;
        internal int VisibleRenderCount;
        internal int InvisibleRenderCount;

        public void Start()
        {
            TotalRenders = 0;
            VisibleRenderCount = 0;
            InvisibleRenderCount = 0;
        }

        internal void RaiseChildrenBecameVisible()
        {
            if(null != ChildrenBecameVisible)
            {
                ChildrenBecameVisible(this, new EventArgs());
            }
        }

        internal void RaiseChildrenBecameInvisible()
        {
            if (null != ChildrenBecameInvisible)
            {
                ChildrenBecameInvisible(this, new EventArgs());
            }
        }
    }
}