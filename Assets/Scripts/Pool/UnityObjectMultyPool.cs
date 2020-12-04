using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PoolSystem
{
    public class UnityObjectMultyPool<TPool, TPoolItem> 
        where TPool : UnityObjectCachePoolBase<TPoolItem> 
        where TPoolItem : MonoBehaviour
    {
        private Dictionary<GameObject, TPool> _poolsDictionary = new Dictionary<GameObject, TPool>();

        public bool ContainsPool(GameObject prefab)
        {
            return _poolsDictionary.ContainsKey(prefab);
        }

        public void PrePoolItems(GameObject prefab, int count)
        {
            var pool = _poolsDictionary[prefab];
            pool.PrePoolItems(count);
        }

        public TPoolItem PopItem(GameObject prefab, Transform parent, Vector3 position)
        {
            return PopItem(prefab, parent, position, Quaternion.identity);
        }

        public TPoolItem PopItem(GameObject prefab, Transform parent, Vector3 position, Quaternion rotation)
        {
            var item = _poolsDictionary[prefab].PopItem(parent, position, rotation);
            return item;
        }

        public void PushItem(TPoolItem item)
        {
            var pool = GetPool(item);
            pool.PushItem(item);
        }

        public void AddPool(TPool pool)
        {
            var prefab = pool.Prefab;
            if (_poolsDictionary.ContainsKey(prefab))
            {
                Debug.LogError($"Pool with prefab {prefab.name} already added");
                return;
            }

            _poolsDictionary.Add(prefab, pool);
        }

        public void RemovePool(GameObject prefab)
        {
            if (!_poolsDictionary.ContainsKey(prefab))
            {
                Debug.LogError($"Pool with prefab {prefab.name} not added");
                return;
            }

            RemovePoolInternal(prefab);
        }

        public void RemoveAllPools()
        {
            var keys = new GameObject[_poolsDictionary.Keys.Count];
            _poolsDictionary.Keys.CopyTo(keys, 0);
            foreach (var key in keys)
            {
                RemovePoolInternal(key);
            }
        }

        private void RemovePoolInternal(GameObject prefab)
        {
            _poolsDictionary.Remove(prefab);
        }

        private TPool GetPool(TPoolItem item)
        {
            TPool pool = null;
            foreach (var p in _poolsDictionary.Values)
            {
                if (!p.IsThisPoolInstance(item))
                    continue;

                pool = p;
            }

            return pool;
        }
    }
}