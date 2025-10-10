using System;

namespace Ambience
{
    [Serializable]
    public class VolumeProfile : Profile
    {
        public override AmbienceType AmbienceType => AmbienceType.Volume;
        
        public HueShift hueShift;
        public override ProfileData[] GetProfileDatas() {
            return new ProfileData[]
            {
                hueShift
            };
        }
    }
}