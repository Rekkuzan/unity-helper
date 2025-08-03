using UniRx;
using UnityEngine;

namespace Rekkuzan.Utilities.UI.Navigation
{
    public class ScreenElementInstantiator : MonoBehaviour
    {
        [SerializeField]
        private ScreenElement _screenElement;
        
        [SerializeField]
        private GameObject _prefab;
        
        [SerializeField]
        private Transform _parent;
        
        [SerializeField, Tooltip("Negate the result of IsInStack")] private bool _negate;

        private GameObject _instance;
        
        private void Awake()
        {
            _screenElement.IsInStack
                .Select(isInStack => _negate ? !isInStack : isInStack)
                .Subscribe(CreateOrDestroy)
                .AddTo(gameObject);
        }

        private void OnDestroy()
        {
            if (_instance == null) return;
            Destroy(_instance);
            _instance = null;
        }

        private void CreateOrDestroy(bool isInStack)
        {
            if (isInStack)
            {
                _instance = Instantiate(_prefab, _parent);
                return;
            }
            
            if (_instance != null)
            {
                
                Destroy(_instance);
                _instance = null;
            }
        }
    }
}