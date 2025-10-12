using System;
using UnityEngine;

namespace Ambience
{
    [Serializable]
    public class Wind : Sound
    {
        public override AmbientSoundType SoundType => AmbientSoundType.Wind;
        public static float TotalIntensity;
        public static int Count;
        public static float CurrIntensity => TotalIntensity / Count;

        public Wind() {
            TotalIntensity = 0;
            Count = 0;
        }
        
        protected override void ProcessInternal(ProfileData tempData) {
            Wind tempProfileData = tempData as Wind;
            TotalIntensity += Intensity;
            Count++;
            tempProfileData.Intensity = CurrIntensity;
        }
    }
}