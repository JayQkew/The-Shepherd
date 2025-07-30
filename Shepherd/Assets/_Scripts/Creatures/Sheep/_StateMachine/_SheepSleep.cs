using System;
using UnityEngine;

[Serializable]
public class _SheepSleep : _SheepBaseState
{
    public Timer sleepTimer;
    public override void EnterState(_SheepStateManager manager) {
        Debug.Log("Entering Sheep Sleep");
        sleepTimer.maxTime = manager.stats.sleepTime.RandomValue();
        sleepTimer.Reset();
    }

    public override void UpdateState(_SheepStateManager manager) {
        sleepTimer.Update();
        if (sleepTimer.IsFinished) {
            manager.SwitchState((manager.RandomState()));
        }
    }

    public override void ExitState(_SheepStateManager manager) {
        Debug.Log("Exiting Sheep Sleep");
    }
}
