using System;
using UnityEngine;

[Serializable]
public class _SheepRun : _SheepBaseState
{
    public override void EnterState(_SheepStateManager manager) {
        Debug.Log("Entering Sheep Run");
    }

    public override void UpdateState(_SheepStateManager manager) {
        Debug.Log("Updating Sheep Run");
    }

    public override void ExitState(_SheepStateManager manager) {
        Debug.Log("Exiting Sheep Run");
    }
}
