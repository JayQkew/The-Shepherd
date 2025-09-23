using System;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;


namespace Creatures.Sheep
{
    [Serializable]
    public class SheepMove : SheepBaseState
    {
        public Vector3 dir;
        public float speed;
        private Rigidbody rb;
        public Timer moveTimer;
    
        public override void EnterState(SheepStateManager manager) {
            manager.gui.PlayAnim("Idle");
            manager.boids.activeBoids = true;
            if (!rb) rb = manager.sheep.rb;
        
            dir = Random.insideUnitCircle.normalized;
            speed = manager.stats.walkSpeed.RandomValue();
        
            moveTimer.SetMaxTime(manager.stats.walkTime.RandomValue());
        }

        public override void UpdateState(SheepStateManager manager) {
            moveTimer.Update();
            manager.gui.UpdateSuppAnims();
            Vector3 moveForce = new Vector3(dir.x, 0, dir.z) * speed;
            if (moveTimer.IsFinished) {
                manager.SwitchState(manager.GetRandomState());
                return;
            }
            rb.AddForce(moveForce, ForceMode.Acceleration);
        }

        public override void ExitState(SheepStateManager manager) {
        }
    }
}
