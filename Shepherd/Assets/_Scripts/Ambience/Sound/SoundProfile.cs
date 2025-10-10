using System;

namespace Ambience
{
    [Serializable]
    public class SoundProfile : Profile
    {
        public override AmbienceType AmbienceType => AmbienceType.Sound;
        
        public Wind wind;
        public Rain rain;
        public Thunder thunder;
        public Leaves leaves;
        public Birds birds;
        public Insects insects;
        
        public override ProfileData[] GetProfileDatas() {
            return new ProfileData[]
            {
                wind,
                rain,
                thunder,
                leaves,
                birds,
                insects,
            };
        }

        public Sound[] GetProfileSounds() {
            return new Sound[]
            {
                wind,
                rain,
                thunder,
                leaves,
                birds,
                insects,
            };
        }
    }
}