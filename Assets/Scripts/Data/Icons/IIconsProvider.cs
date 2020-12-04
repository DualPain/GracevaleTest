using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GracevaleTest.UI
{
    public interface IIconsProvider
    {
        Sprite GetIcon(string name);
    }
}