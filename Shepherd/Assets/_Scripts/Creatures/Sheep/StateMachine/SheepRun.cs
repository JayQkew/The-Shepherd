using System;
using UnityEngine;

[Serializable]
public class SheepRun : SheepBaseState
{
    public Timer runTimer;

    public override void EnterState(SheepStateManager manager) {
        manager.GetComponent<Boids>().activeBoids = true;
        runTimer.maxTime = manager.stats.runTime.RandomValue();
        runTimer.Reset();
        Debug.Log("Enter -- SheepRun");
    }

    public override void UpdateState(SheepStateManager manager) {
        runTimer.Update();
        if (runTimer.IsFinished) {
            manager.SwitchState(manager.GetRandomState());
        }
    }

    public override void ExitState(SheepStateManager manager) {
        manager.GetComponent<Boids>().activeBoids = false;
        Debug.Log("Exit -- SheepRun");
    }
}
