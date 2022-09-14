using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Rekkuzan.Helper
{
    public class DeviceOrientationEvent : MonoBehaviour
    {
        public UnityEvent OnOrientationChanged;
        public UnityEvent OnResolutionChanged;

        public static event System.Action OnOrientationChangedEvent;
        public static event System.Action OnResolutionChangedEvent;

        [SerializeField]
        private float Delay = 0.1f;

        private Vector2 resolution;
        private DeviceOrientation orientation;

        private float _lastTimeChecked = -1;

        void LateUpdate()
        {
            if (Time.time - _lastTimeChecked < Delay)
                return;

            resolution = new Vector2(Screen.width, Screen.height);
            orientation = Input.deviceOrientation;


            if (resolution.x != Screen.width || resolution.y != Screen.height)
            {
                resolution.x = Screen.width;
                resolution.y = Screen.height;
                OnResolutionChangedEvent?.Invoke();
                OnResolutionChanged?.Invoke();
            }

            switch (Input.deviceOrientation)
            {
                case DeviceOrientation.Unknown:
                case DeviceOrientation.FaceUp:
                case DeviceOrientation.FaceDown:
                    break;
                default:
                    if (orientation != Input.deviceOrientation)
                    {
                        orientation = Input.deviceOrientation;
                        OnOrientationChangedEvent?.Invoke();
                        OnOrientationChanged?.Invoke();
                    }
                    break;
            }
        }

    }
}