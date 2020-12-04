using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GracevaleTest.Data;
using GracevaleTest.Logic.Player;

namespace GracevaleTest.Logic
{
    public static class BuffApplicator
    {
        public static void AddBuff(PlayerState playerState, Buff buff)
        {
            playerState.AddBuff(buff.id);

            foreach (var stat in buff.stats)
            {
                playerState.Stats[stat.statId] += stat.value;

                if (stat.statId == StatsId.LIFE_ID)
                {
                    playerState.Stats[StatsId.MAX_HEALTH] += stat.value;
                }
            }
        }

        public static void RemoveBuff(PlayerState playerState, Buff buff)
        {
            playerState.RemoveBuff(buff.id);

            foreach (var stat in buff.stats)
            {
                playerState.Stats[stat.statId] -= stat.value;

                if (stat.statId == StatsId.LIFE_ID)
                {
                    playerState.Stats[StatsId.MAX_HEALTH] -= stat.value;
                }
            }
        }
    }
}