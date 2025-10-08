using System;

namespace Ambience
{
    [Serializable]
    public class SoundProfile : Profile
    {
        public override AmbienceType AmbienceType => AmbienceType.Lighting;

    }
}