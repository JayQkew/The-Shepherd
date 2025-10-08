using System;

namespace Ambience
{
    [Serializable]
    public class LightingProfile : Profile
    {
        public override AmbienceType AmbienceType => AmbienceType.Lighting;
    }
}