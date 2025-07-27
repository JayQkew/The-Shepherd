using System;
using UnityEngine;

[Serializable]
public class SheepEat : SheepBaseState
{
    public Timer eatTimer;
    public override void EnterState(SheepStateManager manager) {
        Debug.Log("Entering Sheep Eat");
        eatTimer.maxTime = manager.stats.idleTime.RandomValue();
    }

    public override void UpdateState(SheepStateManager manager) {
        eatTimer.Update();
        Debug.Log("Updating Sheep Eat");
    }

    public override void ExitState(SheepStateManager manager) {
        Debug.Log("Exiting Sheep Eat");
    }
}
