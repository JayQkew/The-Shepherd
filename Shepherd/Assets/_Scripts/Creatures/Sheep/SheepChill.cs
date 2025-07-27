using UnityEngine;

public class SheepChill : SheepBaseState
{
    public Timer chillTimer;
    public override void EnterState(SheepStateManager manager) {
        Debug.Log("Entering Sheep Chill");
        chillTimer.maxTime = manager.stats.idleTime.RandomValue();
        chillTimer.Reset();
    }

    public override void UpdateState(SheepStateManager manager) {
        chillTimer.Update();
        if (chillTimer.IsFinished) {
            manager.SwitchState(manager.RandomState());
        }
        Debug.Log("Updating Sheep Chill");
    }

    public override void ExitState(SheepStateManager manager) {
        Debug.Log("Exiting Sheep Chill");
    }
}
