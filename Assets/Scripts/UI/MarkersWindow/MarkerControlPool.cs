using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PoolSystem;

namespace GracevaleTest.UI
{
    public class MarkerControlPool : UnityObjectPoolBase<MarkerControl>
    {
        public MarkerControlPool(GameObject prefab, Transform inPoolParent) : base(prefab, inPoolParent)
        {
        }
    }
}