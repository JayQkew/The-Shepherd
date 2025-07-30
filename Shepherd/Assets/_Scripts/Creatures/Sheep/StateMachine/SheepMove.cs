using System;
using UnityEngine;

[Serializable]
public class SheepMove : SheepBaseState
{
    public override void EnterState(SheepStateManager manager) {
        Debug.Log("Enter -- SheepMove");
    }

    public override void UpdateState(SheepStateManager manager) {
        Debug.Log("Update -- SheepMove");
    }

    public override void ExitState(SheepStateManager manager) {
        Debug.Log("Exit -- SheepMove");
    }
}
