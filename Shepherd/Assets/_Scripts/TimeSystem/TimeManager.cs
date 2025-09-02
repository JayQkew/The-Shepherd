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

    private void Update() {
        time.Update();
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
