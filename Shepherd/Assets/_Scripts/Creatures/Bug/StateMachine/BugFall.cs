using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Creatures
{
    [Serializable]
    public class BugFall : BugBaseState
    {
        [SerializeField] private BugFallData data;

        private Rigidbody rb;
        private Vector3 targetDirection;
        private float nextActionTime;
        private bool isIdle;

        public override void EnterState(BugStateManager manager) {
            if (rb == null) rb = manager.rb;
            manager.rb.useGravity = true;
            targetDirection = manager.transform.forward;
            
            PickNewAction();
        }

        public override void UpdateState(BugStateManager manager) {
            if (rb == null) return;
            if (Time.time > nextActionTime) PickNewAction();

            if (isIdle) {
                rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, Vector3.zero, Time.deltaTime * 3f);
                return;
            }
            
            Vector3 wanderDir = manager.WanderDirection(0.3f);
            targetDirection = Vector3.Lerp(targetDirection, wanderDir, Time.deltaTime * data.turnSpeed);
            Vector3 move = targetDirection * data.walkSpeed;
            move.y = rb.linearVelocity.y;
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, move, Time.deltaTime * 3f);
        }

        public override void ExitState(BugStateManager manager) {
            if (manager.rb != null) manager.rb.linearVelocity = Vector3.zero;
        }

        private void PickNewAction() {
            if (isIdle) {
                isIdle = false;
                nextActionTime = Time.time + Random.Range(data.wanderIntervalMin, data.wanderIntervalMax);
            }
            else {
                isIdle = true;
                nextActionTime = Time.time + Random.Range(data.idleDurationMin, data.idleDurationMax);
            }
        }
    }
}