using System;
using UnityEngine;

[Serializable]
public class _SheepEat : _SheepBaseState
{
    public Timer eatTimer;
    public override void EnterState(_SheepStateManager manager) {
        Debug.Log("Entering Sheep Eat");
        eatTimer.maxTime = manager.stats.idleTime.RandomValue();
        eatTimer.Reset();
    }

    public override void UpdateState(_SheepStateManager manager) {
        eatTimer.Update();
        if (eatTimer.IsFinished) {
            manager.SwitchState(manager.RandomState());
        }
    }

    public override void ExitState(_SheepStateManager manager) {
        Debug.Log("Exiting Sheep Eat");
    }
}
