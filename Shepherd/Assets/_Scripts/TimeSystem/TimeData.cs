using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace TimeSystem
{
    [CreateAssetMenu(fileName = "TimeData", menuName = "Time")]
    public class TimeData : ScriptableObject
    {
        public float totalDayTime;
        public DayPhase[] dayPhases = new DayPhase[]
        {
            new DayPhase(DayPhaseName.Sunrise),
            new DayPhase(DayPhaseName.Day),
            new DayPhase(DayPhaseName.Sunset),
            new DayPhase(DayPhaseName.Night)
        };

        private void OnValidate() {
            totalDayTime = 0;
            foreach (DayPhase dayPhase in dayPhases) {
                totalDayTime += dayPhase.timer.maxTime;
            }
        }
    }
}
