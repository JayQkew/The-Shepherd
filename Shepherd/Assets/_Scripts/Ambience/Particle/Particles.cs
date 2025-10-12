using System;
using UnityEngine;

namespace Ambience
{
    [Serializable]
    public abstract class Particles : ProfileData
    {
        public ParticleSystem particles;
        public float intensity;
        
        public abstract void SetIntensity(float intensity);

        public void PlayParticles() {
            particles.Stop();
            if (intensity <= 0) return;
            SetIntensity(intensity);
            particles.Play();
        }
    }
}
