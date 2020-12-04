using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PoolSystem;

namespace GracevaleTest.UI
{
    public class StatControlPool : UnityObjectPoolBase<StatControl>
    {
        public StatControlPool(GameObject prefab, Transform inPoolParent) : base(prefab, inPoolParent)
        {
        }
    }
}