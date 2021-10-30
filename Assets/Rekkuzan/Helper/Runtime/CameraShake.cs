using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rekkuzan.Helper
{
    /// <summary>
    /// Behaviour to shake the camera
    /// </summary>
    public class CameraShake : MonoBehaviour
    {
        #region Singleton

        private CameraShake() { }
        private static CameraShake _instance;

        private void Awake()
        {
            if (_instance != null)
            {
                ConsoleLogger.Warning("Several instance of GameBehaviour");
                Destroy(this);
                return;
            }

            _instance = this;
        }

        #endregion

        [SerializeField] float DecreaseFactor = 1.0f;

        private Vector3 _OriginalPos;
        private float _ShakeDuration = 0f;
        private float _ShakeAmount = 0.7f;

        private bool _isShaking = false;

        private void LateUpdate()
        {
            if (_ShakeDuration <= 0)
                return;
            if (_ShakeDuration > 0)
            {
                transform.localPosition = _OriginalPos + Random.insideUnitSphere * _ShakeAmount * _ShakeDuration * 0.1f;
                _ShakeDuration -= Time.deltaTime * DecreaseFactor;
            }
            if (_ShakeDuration <= 0)
            {
                _ShakeDuration = 0f;
                _isShaking = false;
                transform.localPosition = _OriginalPos;
            }
        }

        /// <summary>
        /// Will start shaking the camera
        /// </summary>
        /// <param name="time">Time to shake</param>
        /// <param name="amount">Amount to shake</param>
        public void StartShakeInternal(float time = 1.0f, float amount = 0.7f)
        {
            if (!_isShaking)
            {
                _OriginalPos = transform.localPosition;
            }

            _ShakeAmount = amount;
            _ShakeDuration = time;
            _isShaking = true;
        }

        /// <summary>
        /// Will start shaking the camera
        /// </summary>
        /// <param name="time">Time to shake</param>
        /// <param name="amount">Amount to shake</param>
        public static void StartShake(float time = 1.0f, float amount = 0.7f)
        {
            _instance.StartShakeInternal(time, amount);
        }
    }
}