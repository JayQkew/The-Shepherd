using System;
using UnityEngine;

[Serializable]
public class SheepRun : SheepBaseState
{
    public Timer runTimer;

    public override void EnterState(SheepStateManager manager) {
        manager.gui.PlayAnim("Idle");
        manager.GetComponent<Boids>().activeBoids = true;
        runTimer.maxTime = manager.stats.runTime.RandomValue();
        runTimer.Reset();
    }

    public override void UpdateState(SheepStateManager manager) {
        runTimer.Update();
        manager.gui.UpdateSuppAnims();
        if (runTimer.IsFinished) {
            manager.SwitchState(manager.GetRandomState());
        }
    }

    public override void ExitState(SheepStateManager manager) {
        manager.GetComponent<Boids>().activeBoids = false;
    }
}
