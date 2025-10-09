using System;

namespace Ambience
{
    [Serializable]
    public class ParticleProfile : Profile
    {
        public override AmbienceType AmbienceType => AmbienceType.Particles;
        public override ProfileData[] GetProfileDatas() {
            throw new NotImplementedException();
        }
    }
}