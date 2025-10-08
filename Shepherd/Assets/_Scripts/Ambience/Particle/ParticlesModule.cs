using System;
using Climate;
using UnityEngine;

namespace Ambience
{
    [Serializable]
    public class ParticlesModule : Module
    {
        public override AmbienceType AmbienceType => AmbienceType.Particles;

        [SerializeField] private Rain rain;
        [SerializeField] private Snow snow;

        public void SetAmbientParticles(float intensity) {
            rain.particles.Stop();
            snow.particles.Stop();
            
            if (intensity <= 0) return;
            
            if (ClimateManager.Instance.globalTemp <= snow.thresh) {
                snow.SetIntensity(intensity);
                snow.particles.Play();
            }
            else {
                rain.SetIntensity(intensity);
                snow.particles.Play();
            }
        }
    }
}
