using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Rekkuzan.Utilities.UI.Navigation
{
    public class ScreenStackDebugger : MonoBehaviour
    {
        [SerializeField]
        private List<ScreenElement> _elements;

        private IDisposable _disposable;
        private void OnEnable()
        {
            _disposable = ScreenUIController.Elements
                .ObserveAdd().AsUnitObservable()
                .Merge(
                    ScreenUIController.Elements.ObserveMove().AsUnitObservable(),
                    ScreenUIController.Elements.ObserveRemove().AsUnitObservable(),
                    ScreenUIController.Elements.ObserveReplace().AsUnitObservable()
                ).ThrottleFrame(0, FrameCountType.EndOfFrame)
                .Select(_ => ScreenUIController.Elements.ToList())
                .Subscribe(RefreshElements);
        }

        private void OnDisable()
        {
            _disposable?.Dispose();
            _disposable = null;
        }

        private void RefreshElements(IList<ScreenElement> elements)
        {
            _elements.Clear();
            _elements.AddRange(elements);
        }
    }
}