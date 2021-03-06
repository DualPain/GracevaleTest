﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PoolSystem;

namespace GracevaleTest.UI
{
    public class PopupMessageControlPool : UnityObjectPoolBase<PopupMessageControl>
    {
        public PopupMessageControlPool(GameObject prefab, Transform inPoolParent) : base(prefab, inPoolParent)
        {
        }
    }
}