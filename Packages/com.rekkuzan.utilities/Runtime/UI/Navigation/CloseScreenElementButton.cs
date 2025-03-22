using Spaice.Replicate.UI;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Rekkuzan.Utilities.UI.Navigation
{
    [RequireComponent(typeof(Button))]
    public class CloseScreenElementButton : MonoBehaviour
    {
        [SerializeField]
        private ScreenElement _screenElement;

        private Button _button;
        
        private void Awake()
        {
            _button = GetComponent<Button>();
            _button
                .OnClickAsObservable()
                .Subscribe(_ => Hide())
                .AddTo(this);
        }

        protected virtual void Hide()
        {
            _screenElement.Close();
        }
    }
}