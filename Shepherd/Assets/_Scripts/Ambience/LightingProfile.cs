using System;
using System.Collections.Generic;

namespace Ambience
{
    [Serializable]
    public class LightingProfile : Profile
    {
        public override AmbienceType AmbienceType => AmbienceType.Lighting;

        public Lighting lighting;
        public Skybox skybox;
        
        public override ProfileData[] GetProfileDatas() {
            return new ProfileData[]
            {
                lighting,
                skybox
            };
        }
    }
}