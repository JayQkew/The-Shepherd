using System;
using UnityEngine;

[Serializable]
public class Night : TimeBaseState
{
    [SerializeField] private float curr;
    [SerializeField] private float span;
    public override void EnterState(TimeManager manager) {
        Debug.Log("Enter Night");
    }

    public override void UpdateState(TimeManager manager) {
        curr += Time.deltaTime;
        if (curr >= span) {
            manager.SwitchState(manager.sunRise);
        }
    }

    public override void ExitState(TimeManager manager) {
        curr = 0;
        manager.currTime = 0;
    }
}
