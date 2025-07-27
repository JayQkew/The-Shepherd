using UnityEngine;

public class SheepSleep : SheepBaseState
{
    public override void EnterState(SheepStateManager manager) {
        Debug.Log("Entering Sheep Sleep");
    }

    public override void UpdateState(SheepStateManager manager) {
        Debug.Log("Updating Sheep Sleep");
    }

    public override void ExitState(SheepStateManager manager) {
        Debug.Log("Exiting Sheep Sleep");
    }
}
