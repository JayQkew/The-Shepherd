using System;
using UnityEngine;

[Serializable]
public class SunSet : TimeBaseState
{
    public Timer time;
    public override void EnterState(TimeManager manager) {
        manager.timeState = TimeState.Sunset;
    }

    public override void UpdateState(TimeManager manager) {
        time.Update();
        if (time.IsFinished) {
            manager.SwitchState(manager.night);
        }
    }

    public override void ExitState(TimeManager manager) {
        time.Reset();
    }
}
