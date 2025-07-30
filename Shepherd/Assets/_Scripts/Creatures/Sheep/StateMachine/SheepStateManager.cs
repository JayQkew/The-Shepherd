using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SheepStateManager : MonoBehaviour
{
    public SheepBaseState currState;
    public SheepStats stats;

    public SheepIdle sheepIdle = new SheepIdle();
    public SheepEat sheepEat = new SheepEat();
    public SheepSleep sheepSleep = new SheepSleep();
    public SheepMove sheepMove = new SheepMove();
    public SheepRun sheepRun = new SheepRun();

    private void Start() {
        currState = sheepIdle;
        currState.EnterState(this);
    }

    private void Update() {
        currState.UpdateState(this);
    }

    public void SwitchState(SheepBaseState newState) {
        currState.ExitState(this);
        currState = newState;
        currState.EnterState(this);
    }

    public SheepBaseState GetRandomState() {
        SheepBaseState[] states =
        {
            sheepIdle,
            sheepEat,
            sheepSleep,
            sheepMove
        };
        
        return states[Random.Range(0, states.Length)];
    }
}

public abstract class SheepBaseState
{
    public abstract void EnterState(SheepStateManager manager);
    public abstract void UpdateState(SheepStateManager manager);
    public abstract void ExitState(SheepStateManager manager);
}