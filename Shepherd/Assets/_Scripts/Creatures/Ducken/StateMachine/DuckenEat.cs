using System;
using UnityEngine;
using Utilities;

namespace Creatures.Ducken
{
    [Serializable]
    public class DuckenEat : DuckenBaseState
    {
        public Timer eatTimer;
        public override void EnterState(DuckenManager manager) {
            manager.food.Eat();
            manager.boid.activeBoids = false;
            eatTimer.SetMaxTime(manager.stats.eatTime.RandomValue());
        }

        public override void UpdateState(DuckenManager manager) {
            eatTimer.Update();
            if (eatTimer.IsFinished) {
                manager.SwitchRandomState();
            }
        }

        public override void ExitState(DuckenManager manager) {
            Debug.Log("Exit -- DuckenEat");
        }
    }
}
