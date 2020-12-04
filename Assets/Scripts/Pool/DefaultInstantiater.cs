using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PoolSystem
{
    public class DefaultInstantiater : IInstantiater
    {
        public void Destroy(GameObject obj)
        {
            UnityEngine.Object.Destroy(obj);
        }

        public GameObject Instantiate(GameObject prefab, Transform parent)
        {
            var obj = Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
            return obj;
        }

        public GameObject Instantiate(GameObject prefab, Vector3 position, Transform parent)
        {
            var obj = Instantiate(prefab, position, Quaternion.identity, parent);
            return obj;
        }

        public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
        {
            var obj = UnityEngine.Object.Instantiate(prefab, position, rotation, parent) as GameObject;
            return obj;
        }
    }
}