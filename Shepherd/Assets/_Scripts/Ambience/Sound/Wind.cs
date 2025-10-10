using System;

namespace Ambience
{
    [Serializable]
    public class Wind : ProfileData
    {
        public AmbientSoundType SoundType => AmbientSoundType.Wind;
        public static float TotalIntensity;
        public static int Count;
        public static float CurrIntensity => TotalIntensity / Count;
        
        public float intensity;
    }
}