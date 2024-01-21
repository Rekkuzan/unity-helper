using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rekkuzan.Helper.UI.List3DElement
{
    /// <summary>
    /// Monobehaviour that handle UI elements of ScrollviewList3DElement
    /// </summary>
    [RequireComponent(typeof(LayoutElement), typeof(RectTransform))]
    public class GameObject3DUIElement : MonoBehaviour
    {
        public GameObject3DRenderTexture Element3D => element3D;

        protected LayoutElement layoutElement;
        protected GameObject3DRenderTexture element3D;
        protected RectTransform selfRect;
        protected RectTransform content;
        protected RectTransform viewport;

        private bool FirstVisibilityCheck = false;

        public RawImage RawImage = null;
        public bool Visible = false;

        public event System.Action<bool> OnVisibilityChanged;

        /// <summary>
        /// Initialize UI element with GameObject3DRenderTexture associated
        /// </summary>
        /// <param name="_Element3D"></param>
        public void Initialize(GameObject3DRenderTexture _Element3D)
        {
            layoutElement = GetComponent<LayoutElement>();
            element3D = _Element3D;

            if (element3D)
                OnVisibilityChanged += element3D.OnVisibilityChanged;

            if (RawImage == null)
            {
                Debug.LogError($"GameObject3DUIElement {this.gameObject.name} have no specified raw image");
            }
        }

        private void OnDestroy()
        {
            if (element3D)
                OnVisibilityChanged -= element3D.OnVisibilityChanged;
            OnVisibilityChanged = null;
        }

        public void UpdateVisibility()
        {
            Visible = -selfRect.anchoredPosition.y - content.anchoredPosition.y > -256 && -selfRect.anchoredPosition.y - content.anchoredPosition.y < viewport.rect.size.y + 256;

            if (this.gameObject.activeSelf != Visible || !FirstVisibilityCheck)
            {
                FirstVisibilityCheck = true;

                this.gameObject.SetActive(Visible);
                OnVisibilityChanged?.Invoke(Visible);
                if (RawImage && element3D)
                    RawImage.texture = element3D.GetRenderTexture();
            }
        }

        public void Optimize(RectTransform _Content, RectTransform _ViewPort)
        {
            selfRect = GetComponent<RectTransform>();
            content = _Content;
            viewport = _ViewPort;
            layoutElement.enabled = false;
        }
    }
}