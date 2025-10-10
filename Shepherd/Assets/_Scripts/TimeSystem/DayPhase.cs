using System;
using Ambience;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Timer = Utilities.Timer;

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

        public DayPhase(DayPhaseName phase) {
            this.phase = phase;
        }

        public DayPhase() {
            
        }

        public DayPhase Clone() {
            DayPhase newPhase = new DayPhase();
            newPhase.phase = phase;
            newPhase.timer = timer;
            newPhase.ambienceSource = ambienceSource;
            newPhase.onPhaseStart = new UnityEvent();
            newPhase.onPhaseEnd = new UnityEvent();
            
            return newPhase;
        }

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
