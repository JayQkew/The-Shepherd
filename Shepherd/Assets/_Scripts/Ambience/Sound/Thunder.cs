using System;

namespace Ambience
{
    [Serializable]
    public class Thunder : Sound
    {
        public override AmbientSoundType SoundType => AmbientSoundType.Thunder;
        public static float TotalIntensity;
        public static int Count;
        public static float CurrIntensity => TotalIntensity / Count;

        public Thunder() {
            TotalIntensity = 0;
            Count = 0;
        }

        protected override void ProcessInternal(ProfileData tempData) {
            TotalIntensity += Intensity;
            Count++;
        }
    }
}