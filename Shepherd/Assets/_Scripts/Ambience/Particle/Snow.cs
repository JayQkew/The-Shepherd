using System;
using UnityEngine;

namespace Ambience
{
    [Serializable]
    public class Snow : WeatherParticles, IWeatherIntensity
    {
        [SerializeField] private SnowParticleData snowData;
        public float thresh;

        public void SetIntensity(float i) {
            intensity = i;
            ParticleSystem.MainModule main = particles.main;
            main.startSizeX = snowData.size.Lerp(intensity);
            main.startSizeY = snowData.size.Lerp(intensity);
            
            ParticleSystem.EmissionModule emission = particles.emission;
            emission.rateOverTime = snowData.emissionRate.Lerp(intensity);
        }
    }
}
