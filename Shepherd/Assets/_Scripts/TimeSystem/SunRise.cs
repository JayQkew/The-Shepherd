using System;
using UnityEngine;

[Serializable]
public class SunRise : TimeBaseState
{
    [SerializeField] private float curr;
    public float span;
    public override void EnterState(TimeManager manager) {
        Debug.Log("Enter SunRise");
        manager.dayCount++;
    }

    public override void UpdateState(TimeManager manager) {
        curr += Time.deltaTime;
        if (curr >= span) {
            manager.SwitchState(manager.day);
        }
    }

    public override void ExitState(TimeManager manager) {
        curr = 0;
    }
}
