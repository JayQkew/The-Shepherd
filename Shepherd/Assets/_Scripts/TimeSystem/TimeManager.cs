using Climate;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Utilities;

namespace TimeSystem
{
    public class TimeManager : MonoBehaviour
    {
        public static TimeManager Instance { get; private set; }
        public uint dayCount;
        public DayPhaseName currPhase;
        public TimeData data;
        [Space(10)]
        public Timer yearTimer;
        public Timer dayTime;
        public DayPhase currDayPhase;
        
        [HideInInspector] public DayPhase[] dayPhases;

        [Space(20)]
        public UnityEvent onDayPhaseChange;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(this);
            }

            dayPhases = new DayPhase[data.dayPhases.Length];
            for (int i = 0; i < dayPhases.Length; i++) {
                dayPhases[i] = data.dayPhases[i].Clone();
            }
        }

        private void Start() {
            currPhase = DayPhaseName.Sunrise;
        }

        private void Update() {
            dayTime.Update();
            yearTimer.Update();
            UpdateDayTime(dayPhases[(int)currPhase]);

            if (dayTime.IsFinished) {
                dayTime.Reset();
            }
        }

        private void UpdateDayTime(DayPhase dayPhase) {
            dayPhase.UpdateTimer();
            if (dayPhase.timer.IsFinished) {
                dayPhase.timer.Reset();
                int nextPhase = ((int)dayPhase.phase + 1) % 4;

                currPhase = (DayPhaseName)nextPhase;
                currDayPhase = dayPhases[nextPhase];

                if (nextPhase == 0) {
                    dayCount++;
                    onDayPhaseChange.Invoke();
                }
            }
        }

        private void TotalTime() {
            if (data != null) {
                dayTime.maxTime = data.totalDayTime;
                yearTimer.maxTime = data.totalDayTime * 12;
            }
        }

        private void OnValidate() {
            if (Instance == null) {
                Instance = this;
            }

            TotalTime();
        }
    }

    public enum DayPhaseName
    {
        Sunrise = 0,
        Day = 1,
        Sunset = 2,
        Night = 3
    }
}