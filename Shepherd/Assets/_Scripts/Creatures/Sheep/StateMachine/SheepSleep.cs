using System;
using UnityEngine;

[Serializable]
public class SheepSleep : SheepBaseState
{
    public Timer sleepTimer;
    public override void EnterState(SheepStateManager manager) {
        manager.gui.PlayAnim("Sleep");
        manager.boids.activeBoids = false;
        sleepTimer.SetMaxTime(manager.stats.sleepTime.RandomValue());
    }

    public override void UpdateState(SheepStateManager manager) {
        sleepTimer.Update();
        if (sleepTimer.IsFinished) {
            manager.SwitchState((manager.GetRandomState()));
        }
    }

    public override void ExitState(SheepStateManager manager) {
    }
}
