using System.Collections.Generic;
using UnityEngine;

namespace Ambience
{
    public abstract class Profile
    {
        public abstract AmbienceType AmbienceType { get;  }
        [SerializeField] private bool use;

        public void AddIfUsed(List<Profile> usedProfiles) {
            if (use) usedProfiles.Add(this);
        }
    }
}
