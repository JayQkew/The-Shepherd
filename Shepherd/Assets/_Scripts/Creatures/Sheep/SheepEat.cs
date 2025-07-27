using System;
using UnityEngine;

[Serializable]
public class SheepEat : SheepBaseState
{
    public Timer eatTimer;
    public override void EnterState(SheepStateManager manager) {
        Debug.Log("Entering Sheep Eat");
        eatTimer.maxTime = manager.stats.idleTime.RandomValue();
        eatTimer.Reset();
    }

    public override void UpdateState(SheepStateManager manager) {
        eatTimer.Update();
        if (eatTimer.IsFinished) {
            manager.SwitchState(manager.RandomState());
        }
    }

    public override void ExitState(SheepStateManager manager) {
        Debug.Log("Exiting Sheep Eat");
    }
}
