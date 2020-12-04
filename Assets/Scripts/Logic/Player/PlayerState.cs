using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NotifyProperty;

namespace GracevaleTest.Logic.Player
{
    public class PlayerState
    {
        private readonly NotifyDictionary<int, float> _statsDictionary = new NotifyDictionary<int, float>();
        private readonly List<int> _buffs = new List<int>();

        public NotifyDictionary<int, float> Stats => _statsDictionary;
        public IReadOnlyList<int> Buffs => _buffs;

        public void AddBuff(int id)
        {
            _buffs.Add(id);
        }

        public bool HasBuff(int id)
        {
            return _buffs.Contains(id);
        }

        public void RemoveBuff(int id)
        {
            _buffs.Remove(id);
        }

        public void ClearBuffs()
        {
            _buffs.Clear();
        }
    }
}