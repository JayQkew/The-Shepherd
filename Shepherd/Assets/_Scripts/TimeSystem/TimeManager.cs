using System;
using UnityEngine;
using UnityEngine.Serialization;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }
    public Timer time;
    public uint dayCount;
    public TimeState timeState;
    private TimeBaseState currState;

    [Header("States")]
    public SunRise sunRise = new SunRise();
    public Day day = new Day();
    public SunSet sunSet = new SunSet();
    public Night night = new Night();
    
    
    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else {
            Destroy(this);
        }
    }

    private void Start() {
        currState = sunRise;
        currState.EnterState(this);
        time.SetMaxTime(sunRise.time.maxTime + day.time.maxTime + night.time.maxTime + sunSet.time.maxTime);
    }

    private void Update() {
        time.Update();
        currState.UpdateState(this);
    }

    public void SwitchState(TimeBaseState newState) {
        currState.ExitState(this);
        currState = newState;
        currState.EnterState(this);
    }

    [ContextMenu("Update Time")]
    public void UpdateMaxTime() {
        Instance = this;
        time.SetMaxTime(sunRise.time.maxTime + day.time.maxTime + night.time.maxTime + sunSet.time.maxTime);
    }    
    private void OnValidate() {
        if (Application.isPlaying && Instance != null) {
            Instance = this;
            time.SetMaxTime(sunRise.time.maxTime + day.time.maxTime + night.time.maxTime + sunSet.time.maxTime);
        }
    }
}

public abstract class TimeBaseState
{
    public abstract void EnterState(TimeManager manager);
    public abstract void UpdateState(TimeManager manager);
    public abstract void ExitState(TimeManager manager);
}

public enum TimeState
{
    Sunrise,
    Day,
    Sunset,
    Night
}
