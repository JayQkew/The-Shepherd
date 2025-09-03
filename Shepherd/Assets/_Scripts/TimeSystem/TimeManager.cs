using System;
using _Scripts.TimeSystem;
using UnityEngine;
using UnityEngine.Serialization;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }
    public Timer time;
    public uint dayCount;
    public DayPhaseName currPhase;
    public SeasonName currSeason;

    public DayPhase[] dayPhases;
    
    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start() {
        currPhase = DayPhaseName.Sunrise;
    }

    private void Update() {
        UpdateTime();
        
        if (time.IsFinished) {
            time.Reset();
        }
    }

    private void UpdateTime() {
        time.Update();
        // switch (currPhase) {
        //     case DayPhaseName.Sunrise:
        //         dayPhases[0].UpdateTimer();
        //         if (dayPhases[0].timer.IsFinished) {
        //             currPhase = DayPhaseName.Day;
        //         }
        //         break;
        //     case DayPhaseName.Day:
        //         dayPhases[1].UpdateTimer();
        //         if (dayPhases[1].timer.IsFinished) {
        //             currPhase = DayPhaseName.Sunset;
        //         }
        //         break;
        //     case DayPhaseName.Sunset:
        //         dayPhases[2].UpdateTimer();
        //         if (dayPhases[2].timer.IsFinished) {
        //             currPhase = DayPhaseName.Night;
        //         }
        //         break;
        //     case DayPhaseName.Night:
        //         dayPhases[3].UpdateTimer();
        //         if (dayPhases[3].timer.IsFinished) {
        //             currPhase = DayPhaseName.Sunrise;
        //         }
        //         break;
        //     default:
        //         throw new ArgumentOutOfRangeException();
        // }
        UpdateDayTime(dayPhases[(int)currPhase]);
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

    public void TestEvents(string test) {
        Debug.Log(test);
    }

    private void SeasonCheck() {
        if (dayCount % 3 == 0) {
            int nextSeason = ((int)currSeason + 1) % 4;
            currSeason = (SeasonName)nextSeason;
        }
    }

    private void TotalTime() {
        time.maxTime = 0;
        foreach (DayPhase dayPhase in dayPhases) {
            time.maxTime += dayPhase.timer.maxTime;
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

public enum SeasonName
{
    Summer = 0,
    Autumn = 1,
    Winter = 2,
    Spring = 3
}
