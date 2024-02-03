using UnityEngine;

namespace Rekkuzan.Utilities
{
    /// <summary>
    /// Utilities to display the current FPS
    /// </summary>
    public class FPSDisplay : MonoBehaviour
    {
        private float deltaTime = 0.0f;
        private int w = Screen.width;
        private int h = Screen.height;
        private Rect rect;
        private GUIStyle style = new GUIStyle();

        [SerializeField] Color ColorText = Color.white;

        private void Awake()
        {
            style.alignment = TextAnchor.LowerCenter;
            style.fontSize = h * 2 / 100;
            style.normal.textColor = ColorText;
            rect = new Rect(0, 0, w, h);
        }

        void Update()
        {
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        }

        void OnGUI()
        {
            float msec = deltaTime * 1000.0f;
            float fps = 1.0f / deltaTime;
            string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
            GUI.Label(rect, text, style);
        }
    }
}