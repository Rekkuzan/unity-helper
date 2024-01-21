using UnityEngine;
using UnityEngine.UI;

namespace Rekkuzan.Helper.Haptic.UI
{
    [RequireComponent(typeof(Button))]
    public class HapticButton : MonoBehaviour
    {
        [SerializeField]
        private HapticHelper.KeyType HapticType;
        private Button _button;

        private void OnEnable()
        {
            if (_button == null)
                _button = GetComponent<Button>();

            _button.onClick.AddListener(PerformHaptic);
        }
        private void OnDisable()
        {
            _button.onClick.RemoveListener(PerformHaptic);
        }

        private void PerformHaptic()
        {
            HapticHelper.HapticFeedback(HapticType);
        }
    }
}