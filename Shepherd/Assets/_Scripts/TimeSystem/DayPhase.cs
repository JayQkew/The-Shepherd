using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace _Scripts.TimeSystem
{
    [Serializable]
    public class DayPhase
    {
        public DayPhaseName phase;
        public Timer timer;
        [Space(20)]
        [SerializeField] private UnityEvent onPhaseStart;
        [SerializeField] private UnityEvent onPhaseEnd;

        public void UpdateTimer() {
            if (!timer.IsFinished) {
                if (timer.Progress == 0) {
                    onPhaseStart.Invoke();
                }
                timer.Update();
            }
            else {
                onPhaseEnd.Invoke();
                timer.Reset();
            }
        }
    }
}
