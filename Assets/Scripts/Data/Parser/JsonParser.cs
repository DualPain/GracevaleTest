using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GracevaleTest.Data.Parser
{
    public class JsonParser : IJsonParser
    {
        public T FromJson<T>(string json)
        {
            return JsonUtility.FromJson<T>(json);
        }

        public string ToJson(object obj)
        {
            return JsonUtility.ToJson(obj);
        }
    }
}