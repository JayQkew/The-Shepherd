using System;

namespace Ambience
{
    [Serializable]
    public class Leaves : Sound
    {
        public override AmbientSoundType SoundType => AmbientSoundType.Leaves;
        public static float TotalIntensity;
        public static int Count;
        public static float CurrIntensity => TotalIntensity / (Count == 0 ? 1 : Count);

        public Leaves() {
            TotalIntensity = 0;
            Count = 0;
        }

        protected override void ProcessInternal(ProfileData tempData) {
            Leaves tempProfileData = tempData as Leaves;
            TotalIntensity += Intensity;
            Count++;
            tempProfileData.Intensity = CurrIntensity;
        }
    }
}