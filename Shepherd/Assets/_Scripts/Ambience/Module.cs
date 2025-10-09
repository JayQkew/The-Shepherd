using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ambience
{
    public abstract class Module
    {
        public abstract AmbienceType AmbienceType { get;  }
        protected List<Profile> Profiles = new();
        [SerializeField] private int numProfiles;

        public void AddProfile(Profile profile) {
            if (Profiles.Contains(profile)) return;
            Profiles.Add(profile);
            numProfiles = Profiles.Count;
        }

        public void RemoveProfile(Profile profile) {
            Profiles.Remove(profile);
            numProfiles = Profiles.Count;
        }

        public virtual void TotalProfiles() {
            ApplyProfiles();
        }

        public abstract void ApplyProfiles();
    }
}
