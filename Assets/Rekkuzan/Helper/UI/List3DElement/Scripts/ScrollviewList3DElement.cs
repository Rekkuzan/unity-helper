using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rekkuzan.Helper.UI.List3DElement
{
    /// <summary>
    /// Abstract behaviour to handle list of 3D element in UI environment
    /// You'll need to create a heritage class to override the method BuildList to feed the actual list of element by instanciating the GameObjectPrefab
    /// Custom 3D/UI behaviour of element should also have their own class heritage
    /// </summary>
    public abstract class ScrollviewList3DElement : MonoBehaviour
    {
        /// <summary>
        /// UI prefab element that will be instanciated inside the UI list
        /// </summary>
        [Header("Prefabs parameters")]
        public GameObject3DUIElement UIPrefab;

        /// <summary>
        /// Prefab of the 3D element that will be render in 3D inside the list
        /// </summary>
        public GameObject3DRenderTexture GameObjectPrefab;

        /// <summary>
        /// Parent transform to spawn the element UI
        /// </summary>
        [Header("References")]
        public Transform ListParentUI;

        /// <summary>
        /// Size of the rendertexture that will render the 3D element
        /// </summary>
        [Header("Settings")]
        public Vector2Int RenderTextureSize = new Vector2Int(512, 512);

        /// <summary>
        /// Should the BuildList method be called manually or in Monobehaviour's method start
        /// Overriding Start method will cancel this behaviour if base.Start() not called
        /// </summary>
        public bool BuildOnStart = true;

        protected readonly List<GameObject3DUIElement> _currentUIElements = new List<GameObject3DUIElement>();
        protected readonly List<GameObject3DRenderTexture> _current3DElements = new List<GameObject3DRenderTexture>();

        /// <summary>
        /// Method that need to be implemented to fetch the data and by instanciating GameObjectPrefab and initialize it
        /// </summary>
        protected abstract void BuildList();

        private Stack<RenderTexture> _renderTextureUnused = new Stack<RenderTexture>();
        private List<RenderTexture> _renderTextureInUse = new List<RenderTexture>();

        protected virtual void Start()
        {
            if (BuildOnStart)
                BuildList();
        }

        protected virtual void OnDestroy()
        {
            while (_renderTextureUnused.Count > 0)
            {
                var e = _renderTextureUnused.Pop();
                if (e)
                    Destroy(e);
            }

            while (_renderTextureInUse.Count > 0)
            {
                var e = _renderTextureInUse[0];
                _renderTextureInUse.RemoveAt(0);
                if (e)
                    Destroy(e);
            }

            while (_current3DElements.Count > 0)
            {
                var e = _current3DElements[0];
                _current3DElements.RemoveAt(0);
                if (e)
                    Destroy(e.gameObject);
            }

        }

        /// <summary>
        /// Request a new render texture from the pool (create a new one if empty)
        /// </summary>
        /// <returns>RenderTexture</returns>
        public RenderTexture GetRenderTexture()
        {
            RenderTexture result;
            if (_renderTextureUnused.Count > 0)
                result = _renderTextureUnused.Pop();
            else
                result = CreateRenderTexture();

            _renderTextureInUse.Add(result);
            return result;
        }


        /// <summary>
        /// Return a render texture to the pool
        /// </summary>
        /// <param name="rt">Render Textre</param>
        public void SaveRenderTexture(RenderTexture rt)
        {
            _renderTextureInUse.Remove(rt);
            _renderTextureUnused.Push(rt);
        }

        private RenderTexture CreateRenderTexture()
        {
            return new RenderTexture(RenderTextureSize.x, RenderTextureSize.y, 16);
        }

    }
}