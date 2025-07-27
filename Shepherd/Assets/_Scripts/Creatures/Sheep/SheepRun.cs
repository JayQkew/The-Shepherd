using UnityEngine;

public class SheepRun : SheepBaseState
{
    public override void EnterState(SheepStateManager manager) {
        Debug.Log("Entering Sheep Run");
    }

    public override void UpdateState(SheepStateManager manager) {
        Debug.Log("Updating Sheep Run");
    }

    public override void ExitState(SheepStateManager manager) {
        Debug.Log("Exiting Sheep Run");
    }
}
