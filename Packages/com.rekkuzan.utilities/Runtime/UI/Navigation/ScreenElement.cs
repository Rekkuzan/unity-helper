using System;
using System.Linq;
using Spaice.Replicate.UI;
using UniRx;
using UnityEngine;

namespace Rekkuzan.Utilities.UI.Navigation
{
    [CreateAssetMenu(fileName = "ScreenElement", menuName = "ScriptableObjects/UIElements/ScreenElement")]
    public class ScreenElement : ScriptableObject
    {
        [SerializeField] private string _name;

        public string Name => _name;

        public IObservable<bool> IsInStack => ScreenUIController.Instance.IsInStack(this);

        public void Open()
        {
            ScreenUIController.Show(this);
        }

        public void Close()
        {
            ScreenUIController.Hide(this);
        }

    }
}
