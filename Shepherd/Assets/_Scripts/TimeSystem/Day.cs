using System;
using UnityEngine;

[Serializable]
public class Day : TimeBaseState
{
    public Timer time;
    public override void EnterState(TimeManager manager) {
        manager.timeState = TimeState.Day;
    }

    public override void UpdateState(TimeManager manager) {
        time.Update();
        if (time.IsFinished) {
            manager.SwitchState(manager.sunSet);
        }
    }

    public override void ExitState(TimeManager manager) {
        time.Reset();
    }
}
