using Rekkuzan.Utilities.UI.Navigation;
using UniRx;
using UnityEngine;

namespace Spaice.Replicate.UI
{
    public class ScreenElementEnabler : MonoBehaviour
    {
        [SerializeField]
        private ScreenElement _screenElement;
        
        [SerializeField]
        private GameObject _target;


        private void Awake()
        {
            _screenElement.IsInStack
                .Subscribe(UpdateIsInStack)
                .AddTo(gameObject);
            
            _target.SetActive(false);
        }

        private void UpdateIsInStack(bool isInStack)
        {
           _target.SetActive(isInStack);
        }
    }
}
