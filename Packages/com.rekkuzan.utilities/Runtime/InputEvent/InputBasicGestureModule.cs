using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace Rekkuzan.Utilities.InputEvent
{
    public class InputBasicGestureModule : MonoBehaviour
    {
        public static event System.Action<Vector2> OnMouseScroll; 

        /// <summary>
        /// Will provide the delta value distance from the previous pinch (Pixel)
        /// </summary>
        public static event System.Action<float> OnPinch;

        /// <summary>
        /// Will provide the delta angle from the last pinch
        /// </summary>
        public static event System.Action<float> OnRotate;

        /// <summary>
        /// Will provide the delta distance of pan with 1 finger on the screen
        /// </summary>
        public static event System.Action<Vector2> OnPan;

        private void Awake()
        {
            EnhancedTouchSupport.Enable();
            TouchSimulation.Enable();
        }

        void Update()
        {
            PerformPinch();
            PerformRotate();
            PerformHoldDrag();
            PerformMouseScroll();
        }

        /// <summary>
        /// Invoke OnMouseScroll when mouseScrollDelta is superior from 0.
        /// </summary>
        private void PerformMouseScroll()
        {
            if(Input.mouseScrollDelta.magnitude > 0.001f)
            {
                OnMouseScroll?.Invoke(Input.mouseScrollDelta);
            }
        }

        /// <summary>
        /// Require 2 touch inputs, evaluating the drag distance
        /// </summary>
        private void PerformPinch()
        {
            if (InputTouch.TouchCount != 2)
                return;

            var touch1 = InputTouch.GetTouchByIndex(0);
            var touch2 = InputTouch.GetTouchByIndex(1);

            if (touch1.phase.ReadValue() != TouchPhase.Moved && touch2.phase.ReadValue() != TouchPhase.Moved)
                return;

            Vector2 touch1Pos = touch1.position.ReadValue();
            Vector2 touch2Pos = touch2.position.ReadValue();

            Vector2 touch1OldPos = touch1Pos - touch1.delta.ReadValue();
            Vector2 touch2OldPos = touch2Pos - touch2.delta.ReadValue();

            float distance = Vector2.Distance(InputTouch.PercentPositionPixel(touch1Pos),
                InputTouch.PercentPositionPixel(touch2Pos));
            float oldDistance = Vector2.Distance(InputTouch.PercentPositionPixel(touch1OldPos),
                InputTouch.PercentPositionPixel(touch2OldPos));

            OnPinch?.Invoke(distance - oldDistance);
        }

        /// <summary>
        /// Require 2 touch inputs, evaluating the rotation
        /// </summary>
        private void PerformRotate()
        {
            if (InputTouch.TouchCount != 2)
                return;

            var touch1 = InputTouch.GetTouchByIndex(0);
            var touch2 = InputTouch.GetTouchByIndex(1);

            if (touch1.phase.ReadValue() != TouchPhase.Moved && touch2.phase.ReadValue() != TouchPhase.Moved)
                return;

            Vector2 touch1Pos = touch1.position.ReadValue();
            Vector2 touch2Pos = touch2.position.ReadValue();

            Vector2 touch1OldPos = touch1Pos - touch1.delta.ReadValue();
            Vector2 touch2OldPos = touch1Pos - touch2.delta.ReadValue();;

            Vector2 dir = (touch1Pos - touch2Pos).normalized;
            Vector2 oldDir = (touch1OldPos - touch2OldPos).normalized;

            OnRotate?.Invoke(Vector2.SignedAngle(dir, oldDir));
        }

        /// <summary>
        /// Require 1 touch hold down, evaluating the drag value
        /// </summary>
        private void PerformHoldDrag()
        {
            if (InputTouch.IsOverUI)
                return;

            if (InputTouch.TouchCount == 1)
            {
                var touch1 = InputTouch.GetTouchByIndex(0);
                if (touch1.phase.ReadValue() == TouchPhase.Moved)
                    OnPan?.Invoke(InputTouch.PercentPositionPixel(touch1.delta.ReadValue()));
            }
            else if (InputTouch.TouchCount == 2)
            {
                var touch1 = InputTouch.GetTouchByIndex(0);
                var touch2 = InputTouch.GetTouchByIndex(1);

                if (touch1.phase.ReadValue() == TouchPhase.Moved && touch2.phase.ReadValue() == TouchPhase.Moved)
                    OnPan?.Invoke(
                        InputTouch.PercentPositionPixel((touch1.delta.ReadValue() + touch2.delta.ReadValue()) * 0.5f));
            }
        }
    }
}