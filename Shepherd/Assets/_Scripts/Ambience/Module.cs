using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace Ambience
{
    public abstract class Module
    {
        public abstract AmbienceType AmbienceType { get;  }
        public List<Profile> Profiles = new();
    }
}
