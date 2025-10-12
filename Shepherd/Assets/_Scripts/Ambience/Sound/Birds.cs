using System;

namespace Ambience
{
    [Serializable]
    public class Birds : Sound
    {
        public override AmbientSoundType SoundType => AmbientSoundType.Birds;
        public static float TotalIntensity;
        public static int Count;
        public static float CurrIntensity => TotalIntensity / Count;

        public Birds() {
            TotalIntensity = 0;
            Count = 0;
        }

        protected override void ProcessInternal(ProfileData tempData) {
            Birds tempProfileData = tempData as Birds;
            TotalIntensity += Intensity;
            Count++;
            tempProfileData.Intensity = CurrIntensity;
        }
    }
}