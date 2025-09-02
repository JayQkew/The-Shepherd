using System;
using _Scripts.TimeSystem;
using UnityEngine;
using UnityEngine.Serialization;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }
    public Timer time;
    public uint dayCount;
    public DayPhaseName dayPhaseName;

    public DayPhase[] dayPhases;
    
    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else {
            Destroy(this);
        }
    }

    private void Update() {
        time.Update();
    }

    [ContextMenu("Update Time")]
    public void UpdateMaxTime() {
        Instance = this;
    }    
    private void OnValidate() {
        if (Application.isPlaying && Instance != null) {
            Instance = this;
        }
    }
}

public enum DayPhaseName
{
    Sunrise,
    Day,
    Sunset,
    Night
}
