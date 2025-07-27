using UnityEngine;

public class SheepWalk : SheepBaseState
{
    public override void EnterState(SheepStateManager manager) {
        Debug.Log("Entering Sheep Walk");
    }

    public override void UpdateState(SheepStateManager manager) {
        Debug.Log("Updating Sheep Walk");
    }

    public override void ExitState(SheepStateManager manager) {
        Debug.Log("Exiting Sheep Walk");
    }
}
