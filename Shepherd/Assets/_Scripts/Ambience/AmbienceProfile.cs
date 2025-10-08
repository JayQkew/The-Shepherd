using System;

namespace Ambience
{
    /// <summary>
    /// This is a component that can be added to anything that affects the overall ambience
    /// </summary>
    [Serializable]
    public class AmbienceProfile
    {
        public SoundProfile soundProfile;
        public LightingProfile lightingProfile;
        public VolumeProfile volumeProfile;
        public ParticleProfile particleProfile;
    }
}
