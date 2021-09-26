using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rekkuzan.Helper.UI.List3DElement
{
    /// <summary>
    /// Custom implementation of LayoutGroup/ContentSizeFitter optimized to work with ScrollviewList3DElement
    /// Not intented to work with a layout rebuild, since it's disabling layout behaviour after building
    /// Will interact with list of GameObject3DUIElement to update thei visibility states
    /// </summary>
    [RequireComponent(typeof(ContentSizeFitter), typeof(LayoutGroup))]
    public class OptimizeScrollViewUtility : MonoBehaviour
    {
        /// <summary>
        /// Should the optimization call on Start or Manually
        /// </summary>
        public bool AutoOptimizeOnStart = true;

        // Start is called before the first frame update
        IEnumerator Start()
        {
            if (AutoOptimizeOnStart)
            {
                // Be sure to wait at least one real frame for the layout to be build
                yield return new WaitForEndOfFrame();
                yield return null;
                Optimize();
            }
        }

        private bool Optimized = false;
        private readonly List<GameObject3DUIElement> childs = new List<GameObject3DUIElement>();

        private void Update()
        {
            if (Optimized)
            {
                childs.ForEach(c => c.UpdateVisibility());
            }
        }

        private RectTransform Content;
        private RectTransform Viewport;

        /// <summary>
        /// Need to be call after layout building to fetch all the list and disable layoutGroup
        /// </summary>
        public void Optimize()
        {
            var c = GetComponent<ContentSizeFitter>();
            var l = GetComponent<LayoutGroup>();
            c.enabled = false;
            l.enabled = false;

            childs.Clear();
            GetComponentsInChildren(childs);
            
            Content = GetComponent<RectTransform>();
            Viewport = transform.parent.GetComponent<RectTransform>();
            childs.ForEach(child => child.Optimize(Content, Viewport));

            Optimized = true;
        }
    }
}