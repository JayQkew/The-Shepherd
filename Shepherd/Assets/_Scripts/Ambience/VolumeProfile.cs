using System;

namespace Ambience
{
    [Serializable]
    public class VolumeProfile : Profile
    {
        public override AmbienceType AmbienceType => AmbienceType.Volume;
        public override ProfileData[] GetProfileDatas() {
            throw new NotImplementedException();
        }
    }
}