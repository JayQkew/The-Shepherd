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
    }

    public override void UpdateState(SheepStateManager manager) {
        idleTimer.Update();
        manager.gui.UpdateChillAnims();
        if (idleTimer.IsFinished) {
            manager.SwitchState(manager.GetRandomState());
        }
    }

    public override void ExitState(SheepStateManager manager) {
    }
}
