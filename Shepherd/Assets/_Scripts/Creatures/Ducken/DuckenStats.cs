using UnityEngine;
using Utilities;

namespace Creatures.Ducken
{
    [CreateAssetMenu(fileName = "DuckenStats", menuName = "Creatures/DuckenStats")]
    public class DuckenStats : ScriptableObject
    {
        public MinMax idleTime;
        public MinMax eatTime;
        public MinMax sleepTime;
        
        [Space(20)]
        public MinMax runTime;
        public MinMax walkTime;
        public MinMax walkSpeed;

        [Space(20)]
        public MinMax duckenThresh;
        public float barkForce;
        public float gravityForce;
    }
}
