using System;
using UnityEngine;

[Serializable]
public class Night : TimeBaseState
{
    public Timer time;
    public override void EnterState(TimeManager manager) {
        manager.timeState = TimeState.Night;
    }

    public override void UpdateState(TimeManager manager) {
        time.Update();
        if (time.IsFinished) {
            manager.SwitchState(manager.sunRise);
        }
    }

    public override void ExitState(TimeManager manager) {
        time.Reset();
        manager.time.Reset();
    }
}
