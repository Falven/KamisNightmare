using UnityEngine;
using System.Collections;

namespace KamisNightmare.Controllers
{
    public class RenderVisibilityNotifier : MonoBehaviour
    {
        public RenderVisibilityReceiver Receiver;

        private void Start()
        {
            Transform root = transform;
            do
                Receiver = (root = root.parent).GetComponent<RenderVisibilityReceiver>();
            while (null == Receiver);

            if (renderer.isVisible)
            {
                Receiver.VisibleRenderCount++;
            }
            else
            {
                Receiver.InvisibleRenderCount++;
            }
            Receiver.TotalRenders++;
        }

        private void OnBecameInvisible()
        {
            Receiver.InvisibleRenderCount++;
            Receiver.VisibleRenderCount--;
            if (Receiver.InvisibleRenderCount == Receiver.TotalRenders)
            {
                Receiver.RaiseChildrenBecameInvisible();
            }
        }

        private void OnBecameVisible()
        {
            Receiver.InvisibleRenderCount--;
            Receiver.VisibleRenderCount++;
            if (Receiver.VisibleRenderCount == Receiver.TotalRenders)
            {
                Receiver.RaiseChildrenBecameVisible();
            }
        }
    }
}