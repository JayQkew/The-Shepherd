using System;
using System.Collections.Generic;
using Climate;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ambience
{
    [Serializable]
    public class ParticlesModule : Module
    {
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
