using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rekkuzan.Helper.UI.List3DElement
{
    /// <summary>
    /// Behaviour for 3D element of ScrollviewList3DElement
    /// Heritage from this class to override basic behaviour, camera settings, visibility change
    /// </summary>
    public class GameObject3DRenderTexture : MonoBehaviour
    {
        protected Camera _extraCameraRendering;
        public ScrollviewList3DElement ScrollView { get; private set; }

        /// <summary>
        /// Defaut camera setting to render 3D element
        /// Can be overrided by changing _extraCameraRendering
        /// </summary>
        public CameraSettings ExtraCameraSettings;

        [System.Serializable]
        public class CameraSettings
        {
            public float FieldOfView = 60;
            public CameraClearFlags ClearFlags = CameraClearFlags.Color;
            public Color BackgroundColor = new Color(0, 0, 0, 0);
            public float NearClip = 0.1f;
            public float FarClip = 100.0f;
            public Vector3 LocalOffset = new Vector3(0, 0, 2);
            public LayerMask CullingMask = ~0;
        }

        /// <summary>
        /// Initialize the element by specifying the ScrollviewList3DElemet 
        /// </summary>
        /// <param name="list"></param>
        public virtual void Initialize(ScrollviewList3DElement list)
        {
            ScrollView = list;
            ScrollView.Add3DElement(this);
            if (_extraCameraRendering == null)
            {
                var GameObjectCamera = new GameObject("Camera");
                _extraCameraRendering = GameObjectCamera.AddComponent<Camera>();
                ApplyCameraSettings();
            }

            Initialize2DElement();
        }

        /// <summary>
        /// Instanciate the UI element as a child of ListParentUI of ScrollviewList3DElement
        /// </summary>
        public virtual void Initialize2DElement()
        {
            if (ScrollView && ScrollView.UIPrefab && ScrollView.ListParentUI)
            {
                // create 2D element
                var tt = Instantiate(ScrollView.UIPrefab, ScrollView.ListParentUI);
                tt.Initialize(this);

                ScrollView.AddUIElement(tt);
            }
        }

        /// <summary>
        /// Apply the default camera settings setup in inspector
        /// </summary>
        public void ApplyCameraSettings()
        {
            _extraCameraRendering.fieldOfView = ExtraCameraSettings.FieldOfView;
            _extraCameraRendering.clearFlags = ExtraCameraSettings.ClearFlags;
            _extraCameraRendering.backgroundColor = ExtraCameraSettings.BackgroundColor;
            _extraCameraRendering.nearClipPlane = ExtraCameraSettings.NearClip;
            _extraCameraRendering.farClipPlane = ExtraCameraSettings.FarClip;
            _extraCameraRendering.transform.parent = transform;
            _extraCameraRendering.transform.localPosition = ExtraCameraSettings.LocalOffset;
            _extraCameraRendering.cullingMask = ExtraCameraSettings.CullingMask;
            _extraCameraRendering.transform.LookAt(transform);
            _extraCameraRendering.gameObject.SetActive(false);
        }

        /// <summary>
        /// Update the visibility of the 3D element inside the list : Disable camera and free the render texture
        /// </summary>
        /// <param name="visible"></param>
        public void OnVisibilityChanged(bool visible)
        {
            if (!_extraCameraRendering)
                return;

            if (visible)
                _extraCameraRendering.targetTexture = ScrollView.GetRenderTexture();
            else if (_extraCameraRendering.targetTexture != null)
                ScrollView.SaveRenderTexture(_extraCameraRendering.targetTexture);

            _extraCameraRendering.gameObject.SetActive(visible);
        }

        /// <summary>
        /// Fetch the current render texture used by the camera
        /// </summary>
        /// <returns>RenderTexture</returns>
        public RenderTexture GetRenderTexture()
        {
            if (_extraCameraRendering)
                return _extraCameraRendering.targetTexture;
            return null;
        }
    }
}