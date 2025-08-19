using System;
using UnityEngine;

[Serializable]
public class SheepEat : SheepBaseState
{
    public Timer eatTimer;
    public override void EnterState(SheepStateManager manager) {
        manager.gui.PlayAnim("Eat");
        manager.GetComponent<Food>()?.Eat();
        manager.GetComponent<Boids>().activeBoids = false;
        eatTimer.SetMaxTime(manager.stats.eatTime.RandomValue());
    }

    public override void UpdateState(SheepStateManager manager) {
        eatTimer.Update();
        if (eatTimer.IsFinished) {
            manager.SwitchState(manager.GetRandomState());
        }
    }

    public override void ExitState(SheepStateManager manager) {
        manager.gui.PlayAnim("Idle");
    }
}
