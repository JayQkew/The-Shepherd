using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Utilities;

namespace Climate
{
    [Serializable]
    public class Season
    {
        public SeasonName season;
        public float dayTemp;
        public float currTemp;
        [SerializeField] private AnimationCurve tempRatio;
        [SerializeField] private MinMax temp;
        [Space(10)]
        public UnityEvent onSeasonStart;
        public UnityEvent onSeasonEnd;

        public float SetTemp() {
            dayTemp = temp.RandomValue();
            return dayTemp;
        }

        public float GetCurrTemp(float dayProgress) {
            currTemp = dayTemp * tempRatio.Evaluate(dayProgress);
            return currTemp;
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
