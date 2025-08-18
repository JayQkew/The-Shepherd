using System;
using UnityEngine;

[Serializable]
public class SunSet : TimeBaseState
{
    [SerializeField] private float curr;
    public float span;
    public override void EnterState(TimeManager manager) {
        manager.timeState = TimeState.Sunset;
    }

    public override void UpdateState(TimeManager manager) {
        curr += Time.deltaTime;
        if (curr >= span) {
            manager.SwitchState(manager.night);
        }
    }

    public override void ExitState(TimeManager manager) {
        curr = 0;
    }
}
