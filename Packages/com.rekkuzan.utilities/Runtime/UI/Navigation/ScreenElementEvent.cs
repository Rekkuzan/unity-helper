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

        [SerializeField] private bool _skipInitialization;

        private void Awake()
        {
            var observable = _screenElement
                .IsInStack
                .Select(isInStack => _negate ? !isInStack : isInStack);

            if (_skipInitialization)
            {
                observable = observable.Skip(1);
            }

            observable.Subscribe(TriggerEvent)
                .AddTo(gameObject);
        }

        private void TriggerEvent(bool isInStack)
        {
            _eventToTrigger?.Invoke(isInStack);
        }
    }
}