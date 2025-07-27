using UnityEngine;

public class SheepEat : SheepBaseState
{
    public override void EnterState(SheepStateManager manager) {
        Debug.Log("Entering Sheep Eat");
    }

    public override void UpdateState(SheepStateManager manager) {
        Debug.Log("Updating Sheep Eat");
    }

    public override void ExitState(SheepStateManager manager) {
        Debug.Log("Exiting Sheep Eat");
    }
}
