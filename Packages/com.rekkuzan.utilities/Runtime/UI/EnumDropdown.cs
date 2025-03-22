using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UniRx;
using UnityEngine;

namespace Rekkuzan.Utilities.UI
{
    [RequireComponent(typeof(TMP_Dropdown))]
    public abstract class EnumDropdown<TEnum> : MonoBehaviour where TEnum : Enum
    {
        private TMP_Dropdown _dropdown;
        private bool _initialized;
        
        private readonly List<TMP_Dropdown.OptionData> _values = new List<TMP_Dropdown.OptionData>();
        private readonly Dictionary<TEnum, int> _valueIndex = new Dictionary<TEnum, int>();

        public IObservable<TEnum> OnValueChanged()
        {
            if (!_initialized)
            {
                Initialize();
            }

            return _dropdown
                .onValueChanged
                .AsObservable()
                .Select(index => _valueIndex.First(keyValue => keyValue.Value == index).Key)
                .DistinctUntilChanged();
        }

        public TEnum Value => _valueIndex.First(keyValue => keyValue.Value == _dropdown.value).Key;

        private void Awake()
        {
            Initialize();
        }

        protected void Initialize()
        {
            if (_initialized)
            {
                return;
            }
            
            _dropdown = GetComponent<TMP_Dropdown>();
            _dropdown.options = _values;
            
            int index = 0;
            foreach (TEnum type in Enum.GetValues(typeof(TEnum)))
            {
                _values.Add(new (type.ToString()));
                _valueIndex[type] = index;
                index++;
            }

            _initialized = true;
        }

        public void SetValue(TEnum value, bool notify = true)
        {
            Initialize();
            if (notify)
            {
                _dropdown.value = _valueIndex[value];   
            }
            else
            {
                _dropdown.SetValueWithoutNotify(_valueIndex[value]);
            }
        }
    }
}