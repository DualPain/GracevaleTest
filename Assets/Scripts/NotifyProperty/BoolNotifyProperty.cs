using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NotifyProperty
{
    public class BoolNotifyProperty : INotifyPropertyReadOnly<bool>
    {
        private bool _value;

        public bool Value
        {
            get => _value;
            set
            {
                if (_value == value)
                    return;

                _value = value;
                OnValueChanged?.Invoke(_value);
            }
        }

        public event Action<bool> OnValueChanged;
    }
}