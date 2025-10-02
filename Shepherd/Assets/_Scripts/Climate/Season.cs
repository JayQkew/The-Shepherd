using System;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

namespace Climate
{
    [Serializable]
    public class Season
    {
        public SeasonName season;
        public MinMax temp;
        [Space(10)]
        public UnityEvent onSeasonStart;
        public UnityEvent onSeasonEnd;
    }
    
    public enum SeasonName
    {
        Summer = 0,
        Autumn = 1,
        Winter = 2,
        Spring = 3
    }
}
