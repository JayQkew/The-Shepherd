using System;
using UnityEngine;

[Serializable]
public class SheepEat : SheepBaseState
{
    public override void EnterState(SheepStateManager manager) {
        Debug.Log("Enter -- SheepEat");
    }

    public override void UpdateState(SheepStateManager manager) {
        Debug.Log("Update -- SheepEat");
    }

    public override void ExitState(SheepStateManager manager) {
        Debug.Log("Exit -- SheepEat");
    }
}
