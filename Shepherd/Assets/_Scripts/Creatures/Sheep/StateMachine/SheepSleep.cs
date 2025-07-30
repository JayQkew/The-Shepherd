using System;
using UnityEngine;

[Serializable]
public class SheepSleep : SheepBaseState
{
    public override void EnterState(SheepStateManager manager) {
        Debug.Log("Enter -- SheepSleep");
    }

    public override void UpdateState(SheepStateManager manager) {
        Debug.Log("Update -- SheepSleep");
    }

    public override void ExitState(SheepStateManager manager) {
        Debug.Log("Exit -- SheepSleep");
    }
}
