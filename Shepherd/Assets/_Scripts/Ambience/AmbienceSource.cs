using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ambience
{
    /// <summary>
    /// This is a component that can be added to anything that affects the overall ambience
    /// </summary>
    [Serializable]
    public class AmbienceSource
    {
        [Tooltip("To help identify where it came from")]
        public string name;
        public SoundProfile soundProfile = new();
        public LightingProfile lightingProfile = new();
        public VolumeProfile volumeProfile = new();
        public ParticleProfile particleProfile = new();

        public List<Profile> UsedProfiles = new();
        private bool initialized;

        public void Init() {
            UsedProfiles.Clear();
            soundProfile.AddIfUsed(UsedProfiles);
            lightingProfile.AddIfUsed(UsedProfiles);
            volumeProfile.AddIfUsed(UsedProfiles);
            particleProfile.AddIfUsed(UsedProfiles);
            
            initialized = true;
            AmbienceManager.Instance.sources.Add(this);
        }
        
        public void Init(string name) {
            this.name = name;
            
            UsedProfiles.Clear();
            soundProfile.AddIfUsed(UsedProfiles);
            lightingProfile.AddIfUsed(UsedProfiles);
            volumeProfile.AddIfUsed(UsedProfiles);
            particleProfile.AddIfUsed(UsedProfiles);
            
            initialized = true;
            AmbienceManager.Instance.sources.Add(this);
        }

        public void Destroy() {
            if (!initialized) return;
            AmbienceManager.Instance.sources.Remove(this);
        }

        /// <summary>
        /// adds the profiles in UsedProfiles into their respective modules
        /// </summary>
        public void Process(List<Module> modules) {
            foreach (Module module in modules) {
                module.Profiles.Clear();
            }
            
            foreach (Profile profile in UsedProfiles) {
                foreach (Module module in modules) {
                    if (module.AmbienceType == profile.AmbienceType) {
                        module.Profiles.Add(profile);
                        break;
                    }
                }
            }
        }
    }
}


