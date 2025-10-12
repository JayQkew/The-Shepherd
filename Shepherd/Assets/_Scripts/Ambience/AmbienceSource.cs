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
            AmbienceManager.Instance.AddSource(this);
        }

        public void Subscribe(string name) {
            this.name = name;

            UsedProfiles.Clear();
            soundProfile.AddIfUsed(UsedProfiles);
            lightingProfile.AddIfUsed(UsedProfiles);
            volumeProfile.AddIfUsed(UsedProfiles);
            particleProfile.AddIfUsed(UsedProfiles);

            initialized = true;
            AmbienceManager.Instance.AddSource(this);
        }

        public void Unsubscribe() {
            if (!initialized) return;
            AmbienceManager.Instance.RemoveSources(this);
        }

        /// <summary>
        /// Adds the profiles in UsedProfiles into their respective modules
        /// </summary>
        public void DelegateProfiles(Module[] modules) {
            foreach (Module module in modules) {
                foreach (Profile profile in UsedProfiles) {
                    if (module.AmbienceType == profile.AmbienceType) {
                        module.AddProfile(profile);
                        break;
                    }
                }

                module.TotalProfiles();
            }
        }

        /// <summary>
        /// Removes the profiles in UsedProfiles from their respective modules
        /// </summary>
        public void BanishProfiles(Module[] modules) {
            foreach (Module module in modules) {
                foreach (Profile profile in UsedProfiles) {
                    if (module.AmbienceType == profile.AmbienceType) {
                        module.RemoveProfile(profile);
                        break;
                    }
                }

                module.TotalProfiles();
            }
        }
    }
}