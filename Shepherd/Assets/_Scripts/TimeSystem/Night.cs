using System;
using UnityEngine;

[Serializable]
public class Night : TimeBaseState
{
    [SerializeField] private float curr;
    public float span;
    public override void EnterState(TimeManager manager) {
        manager.timeState = TimeState.Night;
    }

    public override void UpdateState(TimeManager manager) {
        curr += Time.deltaTime;
        if (curr >= span) {
            manager.SwitchState(manager.sunRise);
        }
    }

    public override void ExitState(TimeManager manager) {
        curr = 0;
        manager.time.Reset();
    }
}
