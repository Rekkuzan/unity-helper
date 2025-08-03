using System;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Rekkuzan.Utilities.UI.Navigation
{
    public class ScreenUIController : Singleton<ScreenUIController>
    {
        private static readonly ReactiveCollection<ScreenElement> _elements = new();
        public static IReadOnlyReactiveCollection<ScreenElement> Elements => _elements;
        
        
        public IObservable<bool> IsInStack(ScreenElement element) => 
            Elements.ObserveCountChanged()
                .StartWith(0)
                .Select(count => count > 0 && Elements.Contains(element))
                .DistinctUntilChanged();

        public static void Show(ScreenElement element)
        {
            if (_elements.Contains(element))
            {
                Debug.LogWarning($"Trying to show element {element.Name}, but it's already on stack.");
                return;
            }
            
            _elements.Add(element);
        }

        public static void Hide(ScreenElement element)
        {
            _elements.Remove(element);
        }

        public static void Clear()
        {
            _elements.Clear();
        }
    }
}
