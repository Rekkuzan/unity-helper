using UnityEngine;
using UnityEngine.EventSystems;

namespace Rekkuzan.Utilities.InputEvent
{
    public class InputTouch : SingletonMonobehaviour<InputTouch>
    {
        public const bool IsMobile =
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
            true;
#else
            false;
#endif

        [SerializeField]
        private KeyCode _secondaryTouchKeyPinch = KeyCode.LeftControl;

        public static int TouchCount
        {
            get
            {
                if (!IsMobile)
                {
                    if (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0))
                    {
                        if (Input.GetKey(KeyCode.LeftControl))
                            return 2;
                        return 1;
                    }
                }

                return Input.touchCount;
            }
        }

        public static Touch GetTouchByIndex(int index)
        {
            if (!IsMobile)
            {
                Touch touch = new Touch()
                {
                    position = Input.mousePosition,
                    phase = TouchPhase.Stationary,
                    deltaTime = Time.time - _lastTimeUpdate,
                    deltaPosition = (Vector2)Input.mousePosition - _lastMousePosition,
                };

                // On editor, moving more than 1 pixel to consider moving
                if (touch.deltaPosition.magnitude > 1)
                    touch.phase = TouchPhase.Moved;

                if (Input.GetMouseButtonDown(0))
                {
                    touch.phase = TouchPhase.Began;
                }

                else if (Input.GetMouseButtonUp(0))
                {
                    touch.phase = TouchPhase.Ended;
                }

                if (Input.GetKey(Instance._secondaryTouchKeyPinch) && index == 1)
                {
                    Vector2 offsetTouch1 = (Vector2)Input.mousePosition - _editorReferenceMiddlePinch;
                    touch.position = _editorReferenceMiddlePinch - offsetTouch1;
                    touch.deltaPosition *= -1;
                }

                return touch;
            }

            return Input.GetTouch(index);
        }

        public static bool IsOverUI
        {
            get
            {
                // Dirty fix 
                // Touch/Mouse up over UI is not registered
                if (TouchCount > 0 && Instance._wasOverUI && GetTouchByIndex(0).phase == TouchPhase.Ended)
                    return true;

                if (!IsMobile)
                    return EventSystem.current && EventSystem.current.IsPointerOverGameObject();

                if (Input.touchCount > 0)
                {
                    return EventSystem.current &&
                           EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
                }

                if (EventSystem.current && EventSystem.current.IsPointerOverGameObject(0))
                    return true;

                return false;
            }
        }

        public static Vector2 PercentPositionPixel(Vector2 screenPosition)
        {
            screenPosition.x /= Screen.width;
            screenPosition.y /= Screen.height;

            return screenPosition;
        }

        private bool _wasOverUI;


        private static float _lastTimeUpdate = 0;
        private static Vector2 _lastMousePosition;
        private static Vector2 _editorReferenceMiddlePinch;

        private void OnEnable()
        {
            _lastTimeUpdate = Time.time;
            _editorReferenceMiddlePinch = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
            if (!IsMobile)
            {
                DeviceOrientationEvent.OnOrientationChangedEvent += OnScreenChanged;
                DeviceOrientationEvent.OnResolutionChangedEvent += OnScreenChanged;
            }
        }

        private void OnScreenChanged()
        {
            _editorReferenceMiddlePinch.x = Screen.width * 0.5F;
            _editorReferenceMiddlePinch.y = Screen.height * 0.5F;
        }

        private void Update()
        {
            if (!IsMobile)
            {
                _lastTimeUpdate = Time.time;
                _lastMousePosition = Input.mousePosition;
            }

            if (TouchCount > 0 && GetTouchByIndex(0).phase != TouchPhase.Ended)
            {
                _wasOverUI = IsOverUI;
            }
        }

    }
}