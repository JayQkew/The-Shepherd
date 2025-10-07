using System;
using Utilities;

namespace Creatures.Sheep
{
    [Serializable]
    public class SheepEat : SheepBaseState
    {
        public Timer eatTimer;
        public override void EnterState(SheepManager manager) {
            manager.gui.PlayAnim("Eat");
            manager.food.Eat();
            manager.boid.activeBoids = false;
            
            manager.emitter.EventReference = manager.fmodEvents.sheepEat;
            manager.emitter.Play();
            
            eatTimer.SetMaxTime(manager.stats.eatTime.RandomValue());
        }

        public override void UpdateState(SheepManager manager) {
            eatTimer.Update();
            if (eatTimer.IsFinished) {
                manager.SwitchState(manager.GetRandomState());
            }
        }

        public override void ExitState(SheepManager manager) {
            manager.emitter.Stop();
            manager.gui.PlayAnim("Idle");
        }
    }
}
