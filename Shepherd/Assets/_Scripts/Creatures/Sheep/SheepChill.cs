using UnityEngine;

public class SheepChill : SheepBaseState
{
    public override void EnterState(SheepStateManager manager) {
        Debug.Log("Entering Sheep Chill");
    }

    public override void UpdateState(SheepStateManager manager) {
        Debug.Log("Updating Sheep Chill");
    }

    public override void ExitState(SheepStateManager manager) {
        Debug.Log("Exiting Sheep Chill");
    }
}
