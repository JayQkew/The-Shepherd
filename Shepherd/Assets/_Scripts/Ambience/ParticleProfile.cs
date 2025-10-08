using System;

namespace Ambience
{
    [Serializable]
    public class ParticleProfile : Profile
    {
        public override AmbienceType AmbienceType => AmbienceType.Lighting;

    }
}