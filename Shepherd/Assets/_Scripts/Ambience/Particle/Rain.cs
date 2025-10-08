using System;
using UnityEngine;

namespace Ambience
{
    [Serializable]
    public class Rain : WeatherParticles, IWeatherIntensity
    {
        [SerializeField] private RainParticleData rainData;

        public void SetIntensity(float i) {
            intensity = i;
            ParticleSystem.MainModule main = particles.main;
            main.startLifetime = rainData.lifetime.Lerp(intensity);
            main.startSpeed = rainData.startSpeed.Lerp(intensity);
            main.startSizeX = rainData.sizeX.Lerp(intensity);
            main.startSizeY = rainData.sizeY.Lerp(intensity);
            
            ParticleSystem.EmissionModule emission = particles.emission;
            emission.rateOverTime = rainData.emissionRate.Lerp(intensity);
        }
    }
}
