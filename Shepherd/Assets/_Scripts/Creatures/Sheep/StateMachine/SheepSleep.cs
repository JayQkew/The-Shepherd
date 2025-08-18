using System;
using UnityEngine;

[Serializable]
public class SheepSleep : SheepBaseState
{
    public Timer sleepTimer;
    public override void EnterState(SheepStateManager manager) {
        manager.gui.PlayAnim("Sleep");
        manager.GetComponent<Boids>().activeBoids = false;
        sleepTimer.maxTime = manager.stats.sleepTime.RandomValue();
        sleepTimer.Reset();
    }

    public override void UpdateState(SheepStateManager manager) {
        sleepTimer.Update();
        if (sleepTimer.IsFinished) {
            manager.SwitchState((manager.GetRandomState()));
        }
    }

    public override void ExitState(SheepStateManager manager) {
        manager.gui.PlayAnim("Idle");
    }
}
