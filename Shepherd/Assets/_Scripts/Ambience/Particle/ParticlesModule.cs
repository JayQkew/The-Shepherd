using System;
using Climate;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ambience
{
    [Serializable]
    public class ParticlesModule : Module
    {
        public override AmbienceType AmbienceType => AmbienceType.Particles;
        public override void TotalProfiles() {
        }

        public override void ApplyProfiles() {
        }

        [FormerlySerializedAs("rain")] [SerializeField] private RainParticle rainParticle;
        [SerializeField] private Snow snow;

        public void SetAmbientParticles(float intensity) {
            rainParticle.particles.Stop();
            snow.particles.Stop();
            
            if (intensity <= 0) return;
            
            if (ClimateManager.Instance.globalTemp <= snow.thresh) {
                snow.SetIntensity(intensity);
                snow.particles.Play();
            }
            else {
                rainParticle.SetIntensity(intensity);
                snow.particles.Play();
            }
        }
    }
}
