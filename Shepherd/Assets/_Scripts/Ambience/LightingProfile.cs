using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace Ambience
{
    [Serializable]
    public class LightingProfile : Profile
    {
        public override AmbienceType AmbienceType => AmbienceType.Lighting;

        public Light light;
        public Skybox skybox;
        
        public override ProfileData[] GetProfileDatas() {
            return new ProfileData[]
            {
                light,
                skybox
            };
        }
    }
}