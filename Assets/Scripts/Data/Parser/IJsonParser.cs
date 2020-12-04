using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GracevaleTest.Data.Parser
{
    public interface IJsonParser
    {
        T FromJson<T>(string json);
        string ToJson(object obj);
    }
}