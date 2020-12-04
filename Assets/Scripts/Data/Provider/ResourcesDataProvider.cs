using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GracevaleTest.Data.Parser;

namespace GracevaleTest.Data
{
    public class ResourcesDataProvider : IDataProvider
    {
        private IJsonParser _jsonParser;

        private Data _data;

        public Data Data => _data ?? (_data = LoadData());

        public ResourcesDataProvider(IJsonParser jsonParser)
        {
            _jsonParser = jsonParser;
        }

        private Data LoadData()
        {
            var dataTextAsset = Resources.Load<TextAsset>("data");
            var data = _jsonParser.FromJson<Data>(dataTextAsset.text);
            return data;
        }
    }
}