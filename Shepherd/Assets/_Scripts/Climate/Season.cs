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
        public float dayTemp;
        [SerializeField] private MinMax temp;
        [Space(10)]
        public UnityEvent onSeasonStart;
        public UnityEvent onSeasonEnd;

        public float SetTemp() {
            dayTemp = temp.RandomValue();
            return dayTemp;
        }
    }
    
    public enum SeasonName
    {
        Summer = 0,
        Autumn = 1,
        Winter = 2,
        Spring = 3
    }
}
