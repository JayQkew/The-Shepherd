using System;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities;

namespace Ambience
{
    [Serializable]
    public class Rain : WeatherParticles
    {
        [SerializeField] private RainParticleData rainData;

        public void SetRainIntensity(float i) {
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
