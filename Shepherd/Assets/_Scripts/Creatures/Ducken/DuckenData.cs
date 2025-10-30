using UnityEngine;
using Utilities;

namespace Creatures.Ducken
{
    [CreateAssetMenu(fileName = "DuckenData", menuName = "Creatures/Ducken")]
    public class DuckenData : AnimalData
    {
        [Header("Ducken Data")]
        [Space(10)]
        public MinMax idleTime;
        public MinMax eatTime;
        public MinMax sleepTime;
        
        [Space(10)]
        public MinMax runTime;
        public MinMax walkTime;
        public MinMax followTime;
        public MinMax walkSpeed;

        [Space(10)]
        public MinMax duckenThresh;
        public float barkForce;
        public float gravityForce;
    }
}
