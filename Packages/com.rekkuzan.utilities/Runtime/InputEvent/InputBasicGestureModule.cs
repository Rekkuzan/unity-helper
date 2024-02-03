using UnityEngine;

namespace Rekkuzan.Utilities.InputEvent
{
    public class InputBasicGestureModule : MonoBehaviour
    {
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

        void Update()
        {
            PerformPinch();
            PerformRotate();
            PerformHoldDrag();
        }

        /// <summary>
        /// Require 2 touch inputs, evaluating the drag distance
        /// </summary>
        private void PerformPinch()
        {
            if (InputTouch.TouchCount != 2)
                return;

            Touch touch1 = InputTouch.GetTouchByIndex(0);
            Touch touch2 = InputTouch.GetTouchByIndex(1);

            if (touch1.phase != TouchPhase.Moved && touch2.phase != TouchPhase.Moved)
                return;

            Vector2 touch1Pos = touch1.position;
            Vector2 touch2Pos = touch2.position;

            Vector2 touch1OldPos = touch1.position - touch1.deltaPosition;
            Vector2 touch2OldPos = touch2.position - touch2.deltaPosition;

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

            Touch touch1 = InputTouch.GetTouchByIndex(0);
            Touch touch2 = InputTouch.GetTouchByIndex(1);

            if (touch1.phase != TouchPhase.Moved && touch2.phase != TouchPhase.Moved)
                return;

            Vector2 touch1Pos = touch1.position;
            Vector2 touch2Pos = touch2.position;

            Vector2 touch1OldPos = touch1.position - touch1.deltaPosition;
            Vector2 touch2OldPos = touch2.position - touch2.deltaPosition;

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
                Touch touch1 = InputTouch.GetTouchByIndex(0);
                if (touch1.phase == TouchPhase.Moved)
                    OnPan?.Invoke(InputTouch.PercentPositionPixel(touch1.deltaPosition));
            }
            else if (InputTouch.TouchCount == 2)
            {
                Touch touch1 = InputTouch.GetTouchByIndex(0);
                Touch touch2 = InputTouch.GetTouchByIndex(1);

                if (touch1.phase == TouchPhase.Moved && touch2.phase == TouchPhase.Moved)
                    OnPan?.Invoke(
                        InputTouch.PercentPositionPixel((touch1.deltaPosition + touch2.deltaPosition) * 0.5f));
            }
        }
    }
}