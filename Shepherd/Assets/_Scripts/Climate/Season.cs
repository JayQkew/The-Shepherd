using System;
using Ambience;
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
        public WeatherTicket[] weatherTickets;
        [Space(10)]
        public AmbienceSource ambienceSource;
        [Space(10)]
        public UnityEvent onSeasonStart;
        public UnityEvent onSeasonEnd;

        public float SetTemp() {
            dayTemp = temp.RandomValue();
            return dayTemp;
        }

        public void Start() {
            onSeasonStart.Invoke();
            ambienceSource.Init(season.ToString());
        }

        public void End() {
            onSeasonEnd.Invoke();
            ambienceSource.Destroy();
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
