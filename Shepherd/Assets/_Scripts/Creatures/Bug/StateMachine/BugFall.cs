using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Creatures
{
    [Serializable]
    public class BugFall : BugBaseState
    {
        [Header("Walk Settings")]
        public float walkSpeed = 1.5f;

        public float turnSpeed = 3f;
        public float wanderIntervalMin = 2f;
        public float wanderIntervalMax = 5f;

        [Header("Idle Settings")]
        public float idleDurationMin = 1f;

        public float idleDurationMax = 3f;

        [Header("Ground Detection")]
        public float raycastDistance = 2f;

        public LayerMask groundMask;

        private Vector3 targetDirection;
        private float nextActionTime;
        private bool isIdle;
        private float noiseSeed;

        public override void EnterState(BugStateManager manager) {
            manager.rb.useGravity = true;
            noiseSeed = Random.value * 100f;
            targetDirection = manager.transform.forward;
            PickNewAction();
        }

        public override void UpdateState(BugStateManager manager) {
            Rigidbody rb = manager.rb;
            if (rb == null) return;

            float t = Time.time;

            // Align to ground
            if (Physics.Raycast(manager.transform.position + Vector3.up * 0.2f, Vector3.down, out RaycastHit hit,
                    raycastDistance, groundMask)) {
                Vector3 surfaceNormal = hit.normal;
                Quaternion alignRotation = Quaternion.FromToRotation(manager.transform.up, surfaceNormal) *
                                           manager.transform.rotation;
                manager.transform.rotation =
                    Quaternion.Slerp(manager.transform.rotation, alignRotation, Time.deltaTime * 6f);
            }

            // Switch between idle and moving
            if (Time.time > nextActionTime)
                PickNewAction();

            if (isIdle) {
                // Stop moving
                rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, Vector3.zero, Time.deltaTime * 3f);
                return;
            }

            // Wander direction (smooth Perlin noise wandering)
            float noiseX = Mathf.PerlinNoise(noiseSeed, t * 0.3f) * 2f - 1f;
            float noiseZ = Mathf.PerlinNoise(noiseSeed + 10f, t * 0.3f) * 2f - 1f;
            Vector3 wanderDir = new Vector3(noiseX, 0, noiseZ).normalized;

            // Smooth turn
            targetDirection = Vector3.Lerp(targetDirection, wanderDir, Time.deltaTime * turnSpeed);

            // Apply walking movement (on ground plane)
            Vector3 move = targetDirection * walkSpeed;
            move.y = rb.linearVelocity.y; // preserve any physics bounce
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, move, Time.deltaTime * 3f);
        }

        public override void ExitState(BugStateManager manager) {
            // Stop completely when leaving this state
            if (manager.rb != null)
                manager.rb.linearVelocity = Vector3.zero;
        }

        private void PickNewAction() {
            if (isIdle) {
                // After idling, start wandering
                isIdle = false;
                nextActionTime = Time.time + Random.Range(wanderIntervalMin, wanderIntervalMax);
            }
            else {
                // After walking, take a break
                isIdle = true;
                nextActionTime = Time.time + Random.Range(idleDurationMin, idleDurationMax);
            }
        }
    }
}