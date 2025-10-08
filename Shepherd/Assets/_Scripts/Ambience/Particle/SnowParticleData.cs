using UnityEngine;
using Utilities;

namespace Ambience
{
    [CreateAssetMenu(fileName = "SnowParticleData", menuName = "Ambience/Particle/Snow")]
    public class SnowParticleData : ScriptableObject
    {
        public MinMax size;
        public MinMaxInt emissionRate;
    }
}
