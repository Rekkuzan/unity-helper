using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Controls;
using TouchPhase = UnityEditor.DeviceSimulation.TouchPhase;

namespace Rekkuzan.Utilities.InputEvent
{
    public class InputTouch : SingletonMonobehaviour<InputTouch>
    {
        [SerializeField]
        private KeyCode _secondaryTouchKeyPinch = KeyCode.LeftControl;

        public static int TouchCount
        {
            get
            {
                var touchCount = 0;
                for (var i = 0; i < Touchscreen.current.touches.Count; i++)
                {
                    touchCount += Touchscreen.current.touches[i].press.isPressed ? 1 : 0;
                }
                
#if UNITY_EDITOR || UNITY_WEBGL
                if (touchCount >= 1)
                {
                    if (Keyboard.current.leftCtrlKey.isPressed)
                        return 2;
                    return 1;
                }
#endif

                return touchCount;
            }
        }

        public static TouchControl GetTouchByIndex(int index)
        {
            return Touchscreen.current.touches[index];
        }

        public static bool IsOverUI
        {
            get
            {
                return EventSystem.current.IsPointerOverGameObject(Touchscreen.current.primaryTouch.touchId.ReadValue());
            }
        }

        public static Vector2 PercentPositionPixel(Vector2 screenPosition)
        {
            screenPosition.x /= Screen.width;
            screenPosition.y /= Screen.height;

            return screenPosition;
        }

        private void Update()
        {
            if (TouchCount > 0 && GetTouchByIndex(0).phase.ReadValue() != UnityEngine.InputSystem.TouchPhase.Canceled)
            {
                _wasOverUI = IsOverUI;
            }
        }

        private bool _wasOverUI;

#if UNITY_EDITOR || UNITY_WEBGL
        private static Vector2 _editorReferenceMiddlePinch;

        private void OnEnable()
        {
            _editorReferenceMiddlePinch = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
            DeviceOrientationEvent.OnOrientationChangedEvent += OnScreenChanged;
            DeviceOrientationEvent.OnResolutionChangedEvent += OnScreenChanged;
        }

        private void OnScreenChanged()
        {
            _editorReferenceMiddlePinch.x = Screen.width * 0.5F;
            _editorReferenceMiddlePinch.y = Screen.height * 0.5F;
        }
#endif
    }
}
