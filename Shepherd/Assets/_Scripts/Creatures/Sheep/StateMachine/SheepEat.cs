using System;
using UnityEngine;

[Serializable]
public class SheepEat : SheepBaseState
{
    public Timer eatTimer;
    public override void EnterState(SheepStateManager manager) {
        eatTimer.maxTime = manager.stats.idleTime.RandomValue();
        eatTimer.Reset();
        Debug.Log("Enter -- SheepEat");
    }

    public override void UpdateState(SheepStateManager manager) {
        eatTimer.Update();
        if (eatTimer.IsFinished) {
            manager.SwitchState(manager.GetRandomState());
        }
    }

    public override void ExitState(SheepStateManager manager) {
        manager.GetComponent<Sheep>().Eat();
        Debug.Log("Exit -- SheepEat");
    }
}
