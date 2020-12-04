using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NotifyProperty
{
    public interface INotifyDictionaryReadOnly<TKey, TValue>
    {
        TValue this[TKey key] { get; }
        int Count { get; }
        bool ContainsKey(TKey key);

        IReadOnlyDictionary<TKey, TValue> Value { get; }

      //  IEnumerator<TKey, TValue> GetEnumerator();

        event Action<TKey, TValue, TValue> ItemChanged;
        event Action<TKey, TValue> ItemAdded;
        event Action<TKey, TValue> ItemRemoved;
        event Action ItemsCleared;
    }

    public class NotifyDictionary<TKey, TValue> : INotifyDictionaryReadOnly<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> _dictionary = new Dictionary<TKey, TValue>();

        public TValue this[TKey key]
        {
            get
            {
                return _dictionary[key];
            }
            set
            {
                var oldValue = _dictionary.ContainsKey(key) ? _dictionary[key] : default;
                _dictionary[key] = value;
                OnItemChanged(key, oldValue, value);
            }
        }

        public int Count => _dictionary.Count;

        public IReadOnlyDictionary<TKey, TValue> Value => _dictionary;

        public event Action<TKey, TValue, TValue> ItemChanged;
        public event Action<TKey, TValue> ItemAdded;
        public event Action<TKey, TValue> ItemRemoved;
        public event Action ItemsCleared;

        public void Add(TKey key, TValue value)
        {
            _dictionary.Add(key, value);            
            OnItemAdded(key, value);
        }

        public void Remove(TKey key)
        {
            var oldValue = _dictionary.ContainsKey(key) ? _dictionary[key] : default;
            _dictionary.Remove(key);
            OnItemRemoved(key, oldValue);
        }

        public void Clear()
        {
            _dictionary.Clear();
            ItemsCleared?.Invoke();
        }

        public bool ContainsKey(TKey key)
        {
            return _dictionary.ContainsKey(key);
        }

        private void OnItemAdded(TKey key, TValue value)
        {
            ItemAdded?.Invoke(key, value);
        }

        private void OnItemRemoved(TKey key, TValue value)
        {
            ItemRemoved?.Invoke(key, value);
        }

        private void OnItemChanged(TKey key, TValue oldValue, TValue newValue)
        {
            ItemChanged?.Invoke(key, oldValue, newValue);
        }
    }
}