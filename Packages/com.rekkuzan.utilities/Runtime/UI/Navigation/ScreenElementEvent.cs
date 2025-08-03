using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace Rekkuzan.Utilities.UI.Navigation
{
    public class ScreenElementEvent : MonoBehaviour
    {
        [SerializeField]
        private ScreenElement _screenElement;

        [SerializeField, Tooltip("Negate the result of IsInStack")] 
        private bool _negate;

        [SerializeField] private UnityEvent<bool> _eventToTrigger;
        
        private void Awake()
        {
            _screenElement.IsInStack
                .Select(isInStack => _negate ? !isInStack : isInStack)
                .Subscribe(TriggerEvent)
                .AddTo(gameObject);
        }

        private void TriggerEvent(bool isInStack)
        {
            _eventToTrigger?.Invoke(isInStack);
        }
    }
}