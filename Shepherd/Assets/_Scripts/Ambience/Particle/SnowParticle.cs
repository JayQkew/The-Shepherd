using System;
using UnityEngine;

namespace Ambience
{
    [Serializable]
    public class SnowParticle : Particles
    {
        public static float TotalIntensity;
        public static int Count;
        public static float CalculatedIntensity => TotalIntensity / Count;
        public SnowParticleData snowData;

        public SnowParticle(SnowParticleData data, ParticleSystem particleSystem) {
            snowData = data;
            particles = particleSystem;
            TotalIntensity = 0;
            Count = 0;
        }
        
        protected override void ProcessInternal(ProfileData tempData) {
            SnowParticle tempProfileData = tempData as SnowParticle;
            TotalIntensity += intensity;
            Count++;
            tempProfileData.intensity = CalculatedIntensity;
        }

        public override void SetIntensity(float i) {
            intensity = i;
            ParticleSystem.MainModule main = particles.main;
            main.startSizeX = snowData.size.Lerp(intensity);
            main.startSizeY = snowData.size.Lerp(intensity);
            
            ParticleSystem.EmissionModule emission = particles.emission;
            emission.rateOverTime = snowData.emissionRate.Lerp(intensity);
        }
    }
}
