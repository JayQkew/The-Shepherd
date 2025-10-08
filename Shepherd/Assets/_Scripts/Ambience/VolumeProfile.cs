using System;

namespace Ambience
{
    [Serializable]
    public class VolumeProfile : Profile
    {
        public override AmbienceType AmbienceType => AmbienceType.Lighting;

    }
}