using System;

namespace Ambience
{
    [Serializable]
    public class SoundProfile : Profile
    {
        public override AmbienceType AmbienceType => AmbienceType.Sound;
        public override ProfileData[] GetProfileDatas() {
            throw new NotImplementedException();
        }
    }
}