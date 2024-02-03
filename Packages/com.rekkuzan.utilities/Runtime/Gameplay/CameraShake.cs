using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Rekkuzan.Utilities
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

        [SerializeField] private float _decreaseFactor = 1.0f;

        private Vector3 _originalPos;
        private float _shakeDuration = 0f;
        private float _shakeAmount = 0.7f;

        private bool _isShaking = false;

        private void LateUpdate()
        {
            if (_shakeDuration <= 0)
                return;
            if (_shakeDuration > 0)
            {
                transform.localPosition = _originalPos + Random.insideUnitSphere * (_shakeAmount * _shakeDuration * 0.1f);
                _shakeDuration -= Time.deltaTime * _decreaseFactor;
            }
            if (_shakeDuration <= 0)
            {
                _shakeDuration = 0f;
                _isShaking = false;
                transform.localPosition = _originalPos;
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
                _originalPos = transform.localPosition;
            }

            _shakeAmount = amount;
            _shakeDuration = time;
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