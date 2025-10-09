using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace Ambience
{
    public abstract class Module
    {
        public abstract AmbienceType AmbienceType { get;  }
        protected List<Profile> Profiles = new();

        public void AddToProfiles(Profile profile) {
            if (Profiles.Contains(profile)) return;
            Profiles.Add(profile);
        }

        public void RemoveFromProfiles(Profile profile) {
            Profiles.Remove(profile);
        }

        public abstract void ProcessProfiles();
    }
}
