using System;
using UnityEngine;

[Serializable]
public class SheepIdle : SheepBaseState
{
    public Timer idleTimer;
    public override void EnterState(SheepStateManager manager) {
        manager.GetComponent<Boids>().activeBoids = false;
        idleTimer.maxTime = manager.stats.idleTime.RandomValue();
        idleTimer.Reset();
        Debug.Log("Enter -- SheepIdle");
    }

    public override void UpdateState(SheepStateManager manager) {
        idleTimer.Update();
        if (idleTimer.IsFinished) {
            manager.SwitchState(manager.GetRandomState());
        }
    }

    public override void ExitState(SheepStateManager manager) {
        Debug.Log("Exit -- SheepIdle");
    }
}
