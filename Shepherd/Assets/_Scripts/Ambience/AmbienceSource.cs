using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace Ambience
{
    /// <summary>
    /// This is a component that can be added to anything that affects the overall ambience
    /// </summary>
    [Serializable]
    public class AmbienceSource
    {
        public SoundProfile soundProfile;
        public LightingProfile lightingProfile;
        public VolumeProfile volumeProfile;
        public ParticleProfile particleProfile;

        public List<Profile> UsedProfiles = new();

        public void Init() {
            UsedProfiles.Clear();
            soundProfile.AddIfUsed(UsedProfiles);
            lightingProfile.AddIfUsed(UsedProfiles);
            volumeProfile.AddIfUsed(UsedProfiles);
            particleProfile.AddIfUsed(UsedProfiles);
            
            AmbienceManager.Instance.ambienceSources.Add(this);
        }
    }
}
