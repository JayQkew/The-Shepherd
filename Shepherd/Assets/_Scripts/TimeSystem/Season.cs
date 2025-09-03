using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace _Scripts.TimeSystem
{
    [Serializable]
    public class Season
    {
        public SeasonName season;
        [Space(10)]
        public UnityEvent onSeasonStart;
        public UnityEvent onSeasonEnd;
    }
}
