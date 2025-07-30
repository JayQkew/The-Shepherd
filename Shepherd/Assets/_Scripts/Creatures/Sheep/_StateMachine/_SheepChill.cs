using System;
using UnityEngine;

[Serializable]
public class _SheepChill : _SheepBaseState
{
    public Timer chillTimer;
    public override void EnterState(_SheepStateManager manager) {
        Debug.Log("Entering Sheep Chill");
        chillTimer.maxTime = manager.stats.chillTime.RandomValue();
        chillTimer.Reset();
    }

    public override void UpdateState(_SheepStateManager manager) {
        chillTimer.Update();
        if (chillTimer.IsFinished) {
            manager.SwitchState(manager.RandomState());
        }
    }

    public override void ExitState(_SheepStateManager manager) {
        Debug.Log("Exiting Sheep Chill");
    }
}
