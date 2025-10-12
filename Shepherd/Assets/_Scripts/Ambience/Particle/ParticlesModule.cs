using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ambience
{
    [Serializable]
    public class ParticlesModule : Module
    {
        public override AmbienceType AmbienceType => AmbienceType.Particles;

        [SerializeField] private RainParticle rainProfileData;
        [SerializeField] private SnowParticle snowProfileData;
        
        public override void TotalProfiles() {
            RainParticle tempRainParticle = new RainParticle(rainProfileData.rainData, rainProfileData.particles);
            SnowParticle tempSnowParticle = new SnowParticle(snowProfileData.snowData, snowProfileData.particles);

            foreach (Profile profile in Profiles) {
                ParticleProfile particleProfile = profile as ParticleProfile;

                if (particleProfile == null) {
                    Debug.LogWarning("ParticleProfile is not a ParticleProfile");
                    continue;
                }

                ProfileData[] profileDatas = particleProfile.GetProfileDatas();

                foreach (ProfileData profileData in profileDatas) {
                    if (profileData.Use) {
                        if (profileData is RainParticle rainParticleData) rainParticleData.Process(tempRainParticle); 
                        else if (profileData is SnowParticle snowParticleData) snowParticleData.Process(tempSnowParticle);
                    }
                }
            }

            rainProfileData = tempRainParticle;
            snowProfileData = tempSnowParticle;

            base.TotalProfiles();
        }

        public override void ApplyProfiles() {
            rainProfileData.PlayParticles();
            snowProfileData.PlayParticles();
        }
    }
}
