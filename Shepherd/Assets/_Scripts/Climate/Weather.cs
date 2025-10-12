using System;
using Ambience;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Climate
{
    [Serializable]
    public class Weather
    {
        [SerializeField] private WeatherTicket data;
        public WeatherType weatherType;
        public float tempDelta;
        public float intensity;
        public AmbienceSource ambienceSource;

        public UnityEvent onWeatherBegin;
        public UnityEvent onWeatherEnd;

        public Weather(WeatherTicket data) {
            this.data = data;
            intensity = Random.Range(0, 1);
            weatherType = data.weatherType;
            tempDelta = data.TempDelta(intensity);
        }

        public void Begin() {
            onWeatherBegin.Invoke();
            ambienceSource.Subscribe(weatherType.ToString());
        }

        public void End() {
            onWeatherEnd.Invoke();
            ambienceSource.Unsubscribe();
        }
    }
}
