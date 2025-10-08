using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace Ambience
{
    public abstract class Module
    {
        public AmbienceType AmbienceType;
        public List<Profile> Profiles = new();
    }
}
