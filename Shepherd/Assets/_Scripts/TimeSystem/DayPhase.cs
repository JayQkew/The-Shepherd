using System;
using Ambience;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

namespace TimeSystem
{
    [Serializable]
    public class DayPhase
    {
        public DayPhaseName phase;
        public Timer timer;
        public AmbienceSource ambienceSource;
        [Space(20)]
        [SerializeField] private UnityEvent onPhaseStart;
        [SerializeField] private UnityEvent onPhaseEnd;

        public void UpdateTimer() {
            if (timer.Progress == 0) {
                onPhaseStart.Invoke();
                ambienceSource.Subscribe(phase.ToString());
            }
            
            timer.Update();
            
            if (timer.IsFinished) {
                onPhaseEnd.Invoke();
                ambienceSource.Unsubscribe();
            }
        }
    }
}
