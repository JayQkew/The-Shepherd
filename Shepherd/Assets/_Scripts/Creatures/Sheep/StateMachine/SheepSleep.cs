using System;
using UnityEngine;

[Serializable]
public class SheepSleep : SheepBaseState
{
    public Timer sleepTimer;
    public override void EnterState(SheepStateManager manager) {
        manager.GetComponent<Boids>().activeBoids = false;
        sleepTimer.maxTime = manager.stats.sleepTime.RandomValue();
        sleepTimer.Reset();
        Debug.Log("Enter -- SheepSleep");
    }

    public override void UpdateState(SheepStateManager manager) {
        sleepTimer.Update();
        if (sleepTimer.IsFinished) {
            manager.SwitchState((manager.GetRandomState()));
        }
    }

    public override void ExitState(SheepStateManager manager) {
        Debug.Log("Exit -- SheepSleep");
    }
}
