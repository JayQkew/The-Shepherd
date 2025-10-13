using System;
using Ambience;
using Unity.VisualScripting;
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
        public Weather(WeatherTicket data) {
            this.data = data;
            intensity = Random.Range(0f, 1f);
            weatherType = data.weatherType;
            tempDelta = data.TempDelta(intensity);
            ambienceSource = data.ambienceSource;
            Begin();
        }

        public void Begin() {
            ambienceSource.Subscribe(weatherType.ToString());
        }

        public void End() {
            ambienceSource.Unsubscribe();
        }
    }
}
