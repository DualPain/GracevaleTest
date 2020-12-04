using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PoolSystem
{
    public interface IPoolCallbackHandler
    {
        void PrePopFromPoolCallback();

        void PostPopFromPoolCallback();

        void PrePushToPoolCallback();

        void PostPushToPoolCallback();
    }
}