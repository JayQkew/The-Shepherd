using UnityEngine;
using UnityEngine.Serialization;
using Utilities;

namespace Creatures.Sheep
{
    [CreateAssetMenu(fileName = "SheepData", menuName = "Creatures/Sheep")]
    public class SheepData : AnimalData
    {
        [Space(25)]
        [Header("Sheep Data")]
        public MinMax idleTime;
        public MinMax eatTime;
        public MinMax sleepTime;
        public MinMax suppAnimTimer;
    
        [Space(10)]
        public MinMax panicTime;
        public MinMax roamTime;
        public MinMax roamSpeed;

        [Space(10)]
        public float woolValue;
        public MinMax woolTime;
        public Timer woolTimer;
        public bool timerSet;
        
        public float savedWoolTimerCurrent;
        public float savedWoolTimerMax;
    }
}
