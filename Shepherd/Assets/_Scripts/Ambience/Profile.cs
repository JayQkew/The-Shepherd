using System.Collections.Generic;
using UnityEngine;

namespace Ambience
{
    public abstract class Profile
    {
        public AmbienceType AmbienceType;
        [SerializeField] private bool use;

        public void AddIfUsed(List<Profile> usedProfiles) {
            if (use) usedProfiles.Add(this);
        }

        public void AddToModule(Module module) {
            if (module.AmbienceType == AmbienceType) module.Profiles.Add(this);
        }
    }
}
