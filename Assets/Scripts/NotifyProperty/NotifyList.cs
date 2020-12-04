using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NotifyProperty
{
    public interface INotifyListReadOnly<out T>
    {
        T this[int index] { get; }
        int Count { get; }

        IReadOnlyList<T> Value { get; }

        IEnumerator<T> GetEnumerator();

        event Action<int, T, T> ItemChanged;
        event Action<int, T> ItemAdded;
        event Action<int, T> ItemRemoved;
        event Action ItemsCleared;
    }

    [Serializable]
    public class NotifyList<T> : INotifyListReadOnly<T>
    {
        [SerializeField]
        private List<T> _list = new List<T>();

        public event Action<int, T, T> ItemChanged;
        public event Action<int, T> ItemAdded;
        public event Action<int, T> ItemRemoved;
        public event Action ItemsCleared;

        public T this[int index]
        {
            get => _list[index];
            set
            {
                var oldValue = _list[index];
                _list[index] = value;
                OnItemChanged(index, oldValue, value);
            }
        }

        public int Count => _list.Count;

        public IReadOnlyList<T> Value => _list;

        public void Add(T value)
        {
            _list.Add(value);
            var index = _list.Count - 1;
            OnItemAdded(index, value);
        }

        public void AddRange(IEnumerable<T> collection)
        {
            var previousCount = _list.Count;
            _list.AddRange(collection);
            var newCount = _list.Count;

            for (var index = previousCount; index < newCount; index++)
            {
                var value = _list[index];
                OnItemAdded(index, value);
            }
        }

        public void Insert(int index, T value)
        {
            _list.Insert(index, value);

            OnItemAdded(index, value);
        }

        public void Remove(T value)
        {
            var index = _list.IndexOf(value);
            _list.Remove(value);
            OnItemRemoved(index, value);
        }

        public void Clear()
        {
            _list.Clear();
            ItemsCleared?.Invoke();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        private void OnItemAdded(int index, T value)
        {
            ItemAdded?.Invoke(index, value);
        }

        private void OnItemRemoved(int index, T value)
        {
            ItemRemoved?.Invoke(index, value);
        }

        private void OnItemChanged(int index, T oldValue, T newValue)
        {
            ItemChanged?.Invoke(index, oldValue, newValue);
        }
    }
}