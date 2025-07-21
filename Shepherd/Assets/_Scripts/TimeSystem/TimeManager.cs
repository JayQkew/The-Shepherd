using System;
using UnityEngine;
using UnityEngine.Serialization;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }
    public float currTime;
    public float maxTime;
    public uint dayCount;
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
        maxTime = sunRise.span + day.span + night.span + sunSet.span;
    }

    private void Update() {
        currTime += Time.deltaTime;
        
        currState.UpdateState(this);
    }

    public void SwitchState(TimeBaseState newState) {
        currState.ExitState(this);
        currState = newState;
        currState.EnterState(this);
    }
}

public abstract class TimeBaseState
{
    public abstract void EnterState(TimeManager manager);
    public abstract void UpdateState(TimeManager manager);
    public abstract void ExitState(TimeManager manager);
}
