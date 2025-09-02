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
            dayCount++;
        }
    }

    private void UpdateTime() {
        time.Update();
        switch (currPhase) {
            case DayPhaseName.Sunrise:
                dayPhases[0].UpdateTimer();
                if (dayPhases[0].timer.IsFinished) {
                    currPhase = DayPhaseName.Day;
                }
                break;
            case DayPhaseName.Day:
                dayPhases[1].UpdateTimer();
                if (dayPhases[1].timer.IsFinished) {
                    currPhase = DayPhaseName.Sunset;
                }
                break;
            case DayPhaseName.Sunset:
                dayPhases[2].UpdateTimer();
                if (dayPhases[2].timer.IsFinished) {
                    currPhase = DayPhaseName.Night;
                }
                break;
            case DayPhaseName.Night:
                dayPhases[3].UpdateTimer();
                if (dayPhases[3].timer.IsFinished) {
                    currPhase = DayPhaseName.Sunrise;
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
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
    Sunrise,
    Day,
    Sunset,
    Night
}

public enum SeasonName
{
    Summer,
    Autumn,
    Winter,
    Spring
}
