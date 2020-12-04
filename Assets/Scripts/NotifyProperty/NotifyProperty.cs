using System;
using System.Collections;
using System.Collections.Generic;

namespace NotifyProperty
{
    public interface INotifyPropertyReadOnly<T>
    {
        T Value { get; }
        event Action<T> OnValueChanged;
    }

    public class NotifyProperty<T> : INotifyPropertyReadOnly<T>
    {
        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                if (_value != null)
                {
                    if (_value.Equals(value))
                        return;
                }
                else if (_value == null)
                {
                    if (value == null)
                        return;
                }

                _value = value;
                OnValueChanged?.Invoke(_value);
            }
        }

        public event Action<T> OnValueChanged;
    }
}