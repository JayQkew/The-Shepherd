using UnityEngine;
using Utilities;

namespace TimeSystem
{
    public class TimeManager : MonoBehaviour
    {
        public static TimeManager Instance { get; private set; }
        public Timer yearTimer;
        public Timer time;
        public uint dayCount;
        public DayPhaseName currPhase;
        public SeasonName currSeason;
        [Space(20)]
        public DayPhase[] dayPhases;
        [Space(20)]
        public Season[] seasons;
    
        private void Awake() {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(this);
            }
        }

        private void Start() {
            currPhase = DayPhaseName.Sunrise;
            seasons[0].onSeasonStart.Invoke();
        }

        private void Update() {
            time.Update();
            yearTimer.Update();
            UpdateDayTime(dayPhases[(int)currPhase]);
        
            if (time.IsFinished) {
                time.Reset();
            }
        }

        private void UpdateDayTime(DayPhase dayPhase) {
            dayPhase.UpdateTimer();
            if (dayPhase.timer.IsFinished) {
                dayPhase.timer.Reset();
                int nextPhase = ((int)dayPhase.phase + 1) % 4;
            
                currPhase = (DayPhaseName)nextPhase;
            
                if (nextPhase == 0) {
                    dayCount++;
                    SeasonCheck();
                }
            }
        }

        private void SeasonCheck() {
            if (dayCount % 3 == 0) {
                seasons[(int)currSeason].onSeasonEnd.Invoke();
                int nextSeason = ((int)currSeason + 1) % 4;
                currSeason = (SeasonName)nextSeason;
                seasons[nextSeason].onSeasonStart.Invoke();
            }
        }

        private void TotalTime() {
            time.maxTime = 0;
            foreach (DayPhase dayPhase in dayPhases) {
                time.maxTime += dayPhase.timer.maxTime;
            }
            yearTimer.maxTime = time.maxTime * 12;
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

    public enum SeasonName
    {
        Summer = 0,
        Autumn = 1,
        Winter = 2,
        Spring = 3
    }
}