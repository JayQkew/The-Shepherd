using System;
using Ambience;
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
        [SerializeField] private MinMax temp;
        [Space(10)]
        public WeatherTicket[] weatherTickets;
        [Space(10)]
        public AmbienceSource ambienceSource;
        [FormerlySerializedAs("onSeasonStart")]
        [Space(10)]
        public UnityEvent onSeasonBegin;
        public UnityEvent onSeasonEnd;

        public float SetTemp() {
            dayTemp = temp.RandomValue();
            return dayTemp;
        }

        public void Begin() {
            onSeasonBegin.Invoke();
            ambienceSource.Subscribe(season.ToString());
        }

        public void End() {
            onSeasonEnd.Invoke();
            ambienceSource.Unsubscribe();
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
