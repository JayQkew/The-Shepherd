using System;
using Ambience;
using UnityEngine;
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

        public Weather(WeatherTicket data) {
            this.data = data;
            intensity = Random.Range(0, 1);
            weatherType = data.weatherType;
            tempDelta = data.TempDelta(intensity);
        }

        public void ApplyAmbiance() {
            if (weatherType.HasFlag(WeatherType.Rainy)) {
                AmbienceManager.Instance.particles.SetAmbientParticles(intensity);
            }
        }
    }
}
