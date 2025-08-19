using System;
using UnityEngine;

[Serializable]
public class SunRise : TimeBaseState
{
    public Timer time;
    public override void EnterState(TimeManager manager) {
        manager.timeState = TimeState.Sunrise;
        manager.dayCount++;
    }

    public override void UpdateState(TimeManager manager) {
        time.Update();
        if (time.IsFinished) {
            manager.SwitchState(manager.day);
        }
    }

    public override void ExitState(TimeManager manager) {
        time.Reset();
    }
}
