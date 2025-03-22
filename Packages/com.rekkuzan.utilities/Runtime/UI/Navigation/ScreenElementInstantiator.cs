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

        private GameObject _instance;
        
        private void Awake()
        {
            _screenElement.IsInStack
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