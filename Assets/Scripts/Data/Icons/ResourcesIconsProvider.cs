using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace GracevaleTest.UI
{
    public class ResourcesIconsProvider : IIconsProvider
    {
        private string _localPath;
        public ResourcesIconsProvider(string localPath)
        {
            _localPath = localPath;
        }

        public Sprite GetIcon(string name)
        {
            var path = Path.Combine(_localPath, name);

            var icon = Resources.Load<Sprite>(path);
            return icon;
        }
    }
}