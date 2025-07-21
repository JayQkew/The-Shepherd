using System;
using UnityEngine;

[Serializable]
public class Day : TimeBaseState
{
    [SerializeField] private float curr;
    public float span;
    public override void EnterState(TimeManager manager) {
        Debug.Log("Enter Day");
    }

    public override void UpdateState(TimeManager manager) {
        curr += Time.deltaTime;
        if (curr >= span) {
            manager.SwitchState(manager.sunSet);
        }
    }

    public override void ExitState(TimeManager manager) {
        curr = 0;
    }
}
