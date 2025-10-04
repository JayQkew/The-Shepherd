using System;
using Utilities;

namespace Creatures.Sheep
{
    [Serializable]
    public class SheepIdle : SheepBaseState
    {
        public Timer idleTimer;
        public override void EnterState(SheepManager manager) {
            manager.gui.PlayAnim("Idle");
            manager.boid.activeBoids = false;
            idleTimer.SetMaxTime(manager.stats.idleTime.RandomValue());
        }

        public override void UpdateState(SheepManager manager) {
            idleTimer.Update();
            manager.gui.UpdateSuppAnims();
            if (idleTimer.IsFinished) {
                manager.SwitchState(manager.GetRandomState());
            }
        }

        public override void ExitState(SheepManager manager) {
        }
    }
}
