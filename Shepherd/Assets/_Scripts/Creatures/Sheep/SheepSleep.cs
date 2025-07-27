using UnityEngine;

public class SheepSleep : SheepBaseState
{
    public Timer sleepTimer;
    public override void EnterState(SheepStateManager manager) {
        Debug.Log("Entering Sheep Sleep");
        sleepTimer.maxTime = manager.stats.sleepTime.RandomValue();
        sleepTimer.Reset();
    }

    public override void UpdateState(SheepStateManager manager) {
        sleepTimer.Update();
        if (sleepTimer.IsFinished) {
            manager.SwitchState((manager.RandomState()));
        }
        Debug.Log("Updating Sheep Sleep");
    }

    public override void ExitState(SheepStateManager manager) {
        Debug.Log("Exiting Sheep Sleep");
    }
}
