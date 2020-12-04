using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PoolSystem
{
    public interface IInstantiater
    {
        GameObject Instantiate(GameObject prefab, Transform parent);
        GameObject Instantiate(GameObject prefab, Vector3 position, Transform parent);
        GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent);

        void Destroy(GameObject obj);
    }
}