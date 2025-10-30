using System;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace Creatures.Ducken
{
    [Serializable]
    public class DuckenMove : DuckenBaseState
    {
        public Vector3 dir;
        public float speed;
        public Timer moveTimer;

        [SerializeField] private bool hasTarget;
        private Vector3 targetPos;
        private Transform target;
        private Rigidbody rb;


        public override void EnterState(DuckenManager manager) {
            // manager.gui.PlayAnim("Idle");
            manager.boid.activeBoids = true;
            if (!rb) rb = manager.rb;

            manager.emitter.EventReference = manager.fmodEvents.duckenWalk;
            manager.emitter.Play();
            
            if (!hasTarget) {
                dir = Random.insideUnitCircle.normalized;
                speed = manager.duckenData.walkSpeed.RandomValue();
            }

            moveTimer.SetMaxTime(hasTarget ? manager.duckenData.followTime.RandomValue() : manager.duckenData.walkTime.RandomValue());
        }

        public override void UpdateState(DuckenManager manager) {
            // manager.gui.UpdateSuppAnims();
            moveTimer.Update();

            if (!hasTarget) {
                if (manager.IsGrounded()) {
                    Vector3 moveForce = new Vector3(dir.x, 0, dir.z) * speed;
                    rb.AddForce(moveForce, ForceMode.Force);
                }
            }
            else {
                Vector3 targetPosition = target != null ? target.position : targetPos;
                
                dir = (targetPosition - rb.position).normalized;
                rb.linearVelocity = dir * speed;

                float distance = Vector3.Distance(rb.position, targetPosition);
                if (distance < 0.5f) {
                    hasTarget = false;
                    manager.SwitchState(manager.duckenIdle);
                }
                
            }

            if (moveTimer.IsFinished) {
                manager.SwitchRandomState();
            }
        }

        public override void ExitState(DuckenManager manager) {
            hasTarget = false;
            manager.emitter.Stop();
        }

        public DuckenMove Target(Vector3 t) {
            hasTarget = true;
            targetPos = t;
            return this;
        }

        public DuckenMove Target(Transform t) {
            hasTarget = true;
            target = t;
            return this;
        }
    }
}