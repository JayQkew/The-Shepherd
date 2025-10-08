using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ambience
{
    [Serializable]
    public class AmbientParticles
    {
        [SerializeField] private Rain rain;
        [SerializeField] private Snow snow;
    }
}
