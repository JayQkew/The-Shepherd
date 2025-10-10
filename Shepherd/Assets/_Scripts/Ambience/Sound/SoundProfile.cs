using System;

namespace Ambience
{
    [Serializable]
    public class SoundProfile : Profile
    {
        public Wind wind;
        public override AmbienceType AmbienceType => AmbienceType.Sound;
        
        public override ProfileData[] GetProfileDatas() {
            return new ProfileData[]{};
        }
    }
}