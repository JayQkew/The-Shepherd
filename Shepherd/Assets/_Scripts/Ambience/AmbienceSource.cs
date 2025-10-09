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

        public LightingProfile lightingProfile = new();
        public VolumeProfile volumeProfile = new();
        public SoundProfile soundProfile = new();
        public ParticleProfile particleProfile = new();

        public List<Profile> UsedProfiles = new();
        private bool initialized;

        public void Subscribe() {
            UsedProfiles.Clear();
            soundProfile.AddIfUsed(UsedProfiles);
            lightingProfile.AddIfUsed(UsedProfiles);
            volumeProfile.AddIfUsed(UsedProfiles);
            particleProfile.AddIfUsed(UsedProfiles);

            initialized = true;
            AmbienceManager.Instance.AddToSources(this);
        }

        public void Subscribe(string name) {
            this.name = name;

            UsedProfiles.Clear();
            soundProfile.AddIfUsed(UsedProfiles);
            lightingProfile.AddIfUsed(UsedProfiles);
            volumeProfile.AddIfUsed(UsedProfiles);
            particleProfile.AddIfUsed(UsedProfiles);

            initialized = true;
            AmbienceManager.Instance.AddToSources(this);
        }

        public void Unsubscribe() {
            if (!initialized) return;
            AmbienceManager.Instance.RemoveFromSources(this);
        }

        /// <summary>
        /// Adds the profiles in UsedProfiles into their respective modules
        /// </summary>
        public void DelegateProfiles(List<Module> modules) {
            foreach (Module module in modules) {
                foreach (Profile profile in UsedProfiles) {
                    if (module.AmbienceType == profile.AmbienceType) {
                        module.AddToProfiles(profile);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Removes the profiles in UsedProfiles from their respective modules
        /// </summary>
        public void BanishProfiles(List<Module> modules) {
            foreach (Module module in modules) {
                foreach (Profile profile in UsedProfiles) {
                    if (module.AmbienceType == profile.AmbienceType) {
                        module.RemoveFromProfiles(profile);
                        break;
                    }
                }
            }
        }
    }
}