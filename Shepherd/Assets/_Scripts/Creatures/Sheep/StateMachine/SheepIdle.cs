using System;
using Utilities;

namespace Creatures.Sheep
{
    [Serializable]
    public class SheepIdle : SheepBaseState
    {
        public Timer idleTimer;
        public override void EnterState(SheepStateManager manager) {
            manager.gui.PlayAnim("Idle");
            manager.boid.activeBoids = false;
            idleTimer.SetMaxTime(manager.stats.idleTime.RandomValue());
        }

        public override void UpdateState(SheepStateManager manager) {
            idleTimer.Update();
            manager.gui.UpdateSuppAnims();
            if (idleTimer.IsFinished) {
                manager.SwitchState(manager.GetRandomState());
            }
        }

        public override void ExitState(SheepStateManager manager) {
        }
    }
}
