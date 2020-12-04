using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PoolSystem
{
    public abstract class UnityObjectCachePoolBase<T> : UnityObjectPoolBase<T> where T : Component
    {
        public UnityObjectCachePoolBase(GameObject prefab, Transform inPoolParent) : base(prefab, inPoolParent) { }

        public UnityObjectCachePoolBase(GameObject prefab, Transform inPoolParent, IInstantiater instantiater) : base(prefab, inPoolParent, instantiater) { }

        private HashSet<T> _spawnedInstances = new HashSet<T>();

        public IReadOnlyCollection<T> SpawnedItems => _spawnedInstances;

        public bool IsThisPoolInstance(T item)
        {
            return _spawnedInstances.Contains(item);
        }

        protected override void PopItemHandler(T item, Transform parent, Vector3 position, Quaternion rotation)
        {
            base.PopItemHandler(item, parent, position, rotation);

            _spawnedInstances.Add(item);
        }

        protected override void PushItemHandler(T item)
        {
            base.PushItemHandler(item);

            _spawnedInstances.Remove(item);
        }
    }
}