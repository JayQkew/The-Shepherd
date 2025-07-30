using System;
using UnityEngine;

[Serializable]
public class SheepIdle : SheepBaseState
{
    public override void EnterState(SheepStateManager manager) {
        Debug.Log("Enter -- SheepIdle");
    }

    public override void UpdateState(SheepStateManager manager) {
        Debug.Log("Update -- SheepIdle");
    }

    public override void ExitState(SheepStateManager manager) {
        Debug.Log("Exit -- SheepIdle");
    }
}
