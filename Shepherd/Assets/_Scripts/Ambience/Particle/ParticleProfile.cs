using System;

namespace Ambience
{
    [Serializable]
    public class ParticleProfile : Profile
    {
        public override AmbienceType AmbienceType => AmbienceType.Particles;
        
        public RainParticle rainParticle;
        public SnowParticle snowParticle;
        
        public override ProfileData[] GetProfileDatas() {
            return new ProfileData[]
            {
                rainParticle,
                snowParticle
            };
        }
    }
}