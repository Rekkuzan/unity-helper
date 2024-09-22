using Rekkuzan.Utilities.Haptic.Android;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rekkuzan.Utilities.Haptic.UI
{
    [RequireComponent(typeof(Button))]
    public class HapticButton : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField]
        private HapticHelper.KeyType HapticType;

        [SerializeField] private bool _performOnRelease = true;
        private Button _button;

        private void OnEnable()
        {
            if (_button == null)
                _button = GetComponent<Button>();

            if (_performOnRelease)
                _button.onClick.AddListener(PerformHaptic);
        }
        private void OnDisable()
        {
            if (_performOnRelease)
                _button.onClick.RemoveListener(PerformHaptic);
        }

        private void PerformHaptic()
        {
            HapticHelper.HapticFeedback(HapticType);
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            if (_performOnRelease)
                return;

            PerformHaptic();
        }
    }
}