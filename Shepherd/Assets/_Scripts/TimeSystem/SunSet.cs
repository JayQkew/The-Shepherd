using System;
using UnityEngine;

[Serializable]
public class SunSet : TimeBaseState
{
    [SerializeField] private float curr;
    [SerializeField] private float span;
    public override void EnterState(TimeManager manager) {
        Debug.Log("Enter SunSet");
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
