using UnityEngine;
using UnityEngine.Serialization;
using Utilities;

namespace Creatures.Sheep
{
    [CreateAssetMenu(fileName = "SheepStats", menuName = "Creatures/SheepStats")]
    public class SheepStats : ScriptableObject
    {
        public MinMax idleTime;
        public MinMax eatTime;
        public MinMax sleepTime;
        public MinMax suppAnimTimer;
    
        [FormerlySerializedAs("runTime")]
        [Space(20)]
        public MinMax panicTime;
        public MinMax roamTime;
        public float roamSpeed;

        [Space(20)]
        public MinMax woolTime;
    }
}
