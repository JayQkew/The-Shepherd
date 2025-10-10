using System;

namespace Ambience
{
    [Serializable]
    public class Insects : Sound
    {
        public override AmbientSoundType SoundType => AmbientSoundType.Insects;
        public static float TotalIntensity;
        public static int Count;
        public static float CurrIntensity => TotalIntensity / Count;

        public Insects() {
            TotalIntensity = 0;
            Count = 0;
        }
    }
}