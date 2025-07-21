using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }
    [SerializeField] private TimeState state;
    public float currTime;
    public uint dayCount;

    private TimeBaseState currState;
    
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
        currTime += Time.deltaTime;
        
        currState.UpdateState(this);
    }
}

[Serializable]
public abstract class TimeBaseState
{
    public abstract void EnterState(TimeManager manager);
    public abstract void UpdateState(TimeManager manager);
    public abstract void ExitState(TimeManager manager);
}

public enum TimeState
{
    SunRise,
    Day,
    SunSet,
    Night
}
