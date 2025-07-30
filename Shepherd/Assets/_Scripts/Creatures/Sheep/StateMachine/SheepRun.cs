using System;
using UnityEngine;

[Serializable]
public class SheepRun : SheepBaseState
{
    public override void EnterState(SheepStateManager manager) {
        Debug.Log("Enter -- SheepRun");
    }

    public override void UpdateState(SheepStateManager manager) {
    }

    public override void ExitState(SheepStateManager manager) {
        Debug.Log("Exit -- SheepRun");
    }
}
