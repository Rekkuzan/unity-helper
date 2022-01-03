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
        private bool Optimized = false;
        private readonly List<GameObject3DUIElement> childs = new List<GameObject3DUIElement>();

        private void Update()
        {
            if (Optimized)
            {
                childs.ForEach(c =>
                {
                    if (c && c.gameObject) 
                        c.UpdateVisibility();
                });
            }
        }

        private RectTransform Content;
        private RectTransform Viewport;

        public event System.Action OnOptimize;

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
            OnOptimize?.Invoke();
        }

        public void ResetLayout()
        {
            var c = GetComponent<ContentSizeFitter>();
            var l = GetComponent<LayoutGroup>();
            c.enabled = true;
            l.enabled = true;

            childs.Clear();

            Optimized = false;
        }

        public void OptimizeNextFrame()
        {
            StartCoroutine(WaitOneFrame(Optimize));
        }

        IEnumerator WaitOneFrame(System.Action callback)
        {
            // actually wait 3 frames
            // layout need more than one frame to build for some reasons
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            var s = GetComponentInParent<ScrollRect>();
           var t = s.viewport.transform.Find("CENTER");
            s.ScrollToCeneter(t.GetComponent<RectTransform>());
            callback?.Invoke();
        }
    }
}