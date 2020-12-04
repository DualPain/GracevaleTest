using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PoolSystem
{
    public abstract class UnityObjectPoolBase<T> where T : Component
    {
        private Stack<T> _itemsStack = new Stack<T>();

        private GameObject _prefab;
        private Transform _inPoolParent;

        private IInstantiater _instantiater;

        public GameObject Prefab => _prefab;

        public UnityObjectPoolBase(GameObject prefab, Transform inPoolParent)
        {
            _prefab = prefab;
            _inPoolParent = inPoolParent;
            _instantiater = new DefaultInstantiater();
        }

        public UnityObjectPoolBase(GameObject prefab, Transform inPoolParent, IInstantiater instantiater)
        {
            _prefab = prefab;
            _inPoolParent = inPoolParent;
            _instantiater = instantiater;
        }

        public void PrePoolItems(int count)
        {
            for (var index = 0; index < count; index++)
            {
                var item = CreateItem(_inPoolParent, Vector3.zero, Quaternion.identity);
                PushItem(item);
            }
        }

        public T PopItem(Transform parent)
        {
            return PopItem(parent, Vector3.zero, Quaternion.identity);
        }

        public T PopItem(Transform parent, Vector3 position)
        {
            return PopItem(parent, position, Quaternion.identity);
        }

        public T PopItem(Transform parent, Vector3 position, Quaternion rotation)
        {
            T item = null;
            if (_itemsStack.Count == 0)
            {
                item = CreateItem(parent, position, rotation);
            }
            else
            {
                item = _itemsStack.Pop();
            }

            if (item is IPoolCallbackHandler prePopFromPoolCallbackHandler)
            {
                prePopFromPoolCallbackHandler.PrePopFromPoolCallback();
            }

            PopItemHandler(item, parent, position, rotation);

            if (item is IPoolCallbackHandler postPopFromPoolCallbackHandler)
            {
                postPopFromPoolCallbackHandler.PostPopFromPoolCallback();
            }

            return item;
        }

        public void PushItem(T item)
        {
            if (item is IPoolCallbackHandler prePushToPoolCallbackHandler)
            {
                prePushToPoolCallbackHandler.PrePushToPoolCallback();
            }

            PushItemHandler(item);

            if (item is IPoolCallbackHandler postPushToPoolCallbackHandler)
            {
                postPushToPoolCallbackHandler.PostPushToPoolCallback();
            }

            _itemsStack.Push(item);
        }

        protected T CreateItem(Transform parent, Vector3 position, Quaternion rotation)
        {
            var obj = _instantiater.Instantiate(_prefab, position, rotation, parent);
            var component = obj.GetComponent<T>();

            return component;
        }

        protected void DestroyItem(T item)
        {
            _instantiater.Destroy(item.gameObject);
        }

        protected virtual void PopItemHandler(T item, Transform parent, Vector3 position, Quaternion rotation)
        {
            item.transform.SetParent(parent);
            item.transform.position = position;
            item.transform.rotation = rotation;
            item.gameObject.SetActive(true);
        }

        protected virtual void PushItemHandler(T item)
        {
            item.gameObject.SetActive(false);
            item.transform.SetParent(_inPoolParent);
        }
    }
}