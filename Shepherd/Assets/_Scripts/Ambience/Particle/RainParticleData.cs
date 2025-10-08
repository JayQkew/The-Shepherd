using UnityEngine;
using Utilities;

namespace Ambience
{
    [CreateAssetMenu(fileName = "RainParticleData", menuName = "Ambience/Particle/Rain")]
    public class RainParticleData : ScriptableObject
    {
        public MinMax lifetime;
        public MinMax startSpeed;
        public MinMaxInt emissionRate;
        [Space(10)]
        public MinMax sizeX;
        public MinMax sizeY;
    }
}
