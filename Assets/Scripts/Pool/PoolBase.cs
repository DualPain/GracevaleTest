using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PoolSystem
{
    public abstract class PoolBase<T> where T : class
    {
        private Stack<T> _itemsStack = new Stack<T>();

        public void PrePoolItems(int count)
        {
            for (var index = 0; index < count; index++)
            {
                var item = CreateItem();
                PushItem(item);
            }
        }

        public T PopItem()
        {
            T item = null;
            if (_itemsStack.Count == 0)
            {
                item = CreateItem();
            }
            else
            {
                item = _itemsStack.Pop();
            }

            if (item is IPoolCallbackHandler prePopFromPoolCallbackHandler)
            {
                prePopFromPoolCallbackHandler.PrePopFromPoolCallback();
            }

            PopItemHandler(item);

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

        protected abstract T CreateItem();

        protected abstract void DestroyItem(T item);

        protected virtual void PopItemHandler(T item) { }

        protected virtual void PushItemHandler(T item) { }
    }
}