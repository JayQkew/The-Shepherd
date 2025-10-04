using System;
using Utilities;

namespace Creatures.Sheep
{
    [Serializable]
    public class SheepRun : SheepBaseState
    {
        public Timer runTimer;

        public override void EnterState(SheepStateManager manager) {
            manager.gui.PlayAnim("Idle");
            manager.boid.activeBoids = true;
            runTimer.SetMaxTime(manager.stats.runTime.RandomValue());
        }

        public override void UpdateState(SheepStateManager manager) {
            runTimer.Update();
            manager.gui.UpdateSuppAnims();
            if (runTimer.IsFinished) {
                manager.SwitchState(manager.GetRandomState());
            }
        }

        public override void ExitState(SheepStateManager manager) {
            manager.boid.activeBoids = false;
        }
    }
}
