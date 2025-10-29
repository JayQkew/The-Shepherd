using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Creatures
{
    [Serializable]
    public class BugFly : BugBaseState
    {
        [SerializeField] private BugFlyData data;

        private Rigidbody rb;
        private Vector3 targetDirection;
        private float noiseSeed;
        private float t;

        public override void EnterState(BugStateManager manager) {
            rb = manager.rb;
            manager.rb.useGravity = false;
            noiseSeed = Random.value * 100f;
            targetDirection = manager.transform.forward;
        }

        public override void UpdateState(BugStateManager manager) {
            if (rb == null) return;
            t = Time.time;

            Vector3 wanderDir = WanderDirection();
            targetDirection = Vector3.Lerp(targetDirection, wanderDir, Time.deltaTime * data.turnSpeed);
            Hover(manager);
            float flutter = Mathf.Sin(t * data.flutterFrequency) * data.flutterAmplitude;

            Vector3 horizontalVelocity = targetDirection * data.speed;
            Vector3 currentVelocity = rb.linearVelocity;
            Vector3 targetVelocity = new Vector3(
                horizontalVelocity.x,
                currentVelocity.y + flutter, 
                horizontalVelocity.z);

            rb.linearVelocity = Vector3.Lerp(currentVelocity, targetVelocity, Time.deltaTime * 3f);
        }

        public override void ExitState(BugStateManager manager) {
        }

        /// <summary>
        /// using perlin noise to generate a random direction
        /// </summary>
        private Vector3 WanderDirection() {
            float x = Mathf.PerlinNoise(noiseSeed, t * 0.5f) * 2f - 1f;
            float z = Mathf.PerlinNoise(noiseSeed + 10f, t * 0.5f) * 2f - 1f;
            return new Vector3(x, 0, z).normalized;
        }

        private void Hover(BugStateManager manager) {
            float currentHeight = manager.transform.position.y;
            bool foundGround = Physics.Raycast(manager.transform.position, Vector3.down, out RaycastHit hit,
                data.raycastDistance, data.groundMask);

            if (foundGround) {
                var groundHeight = hit.point.y;
                float desiredHeight = groundHeight + data.targetHeight;
                float heightError = desiredHeight - currentHeight;

                rb.AddForce(Vector3.up * (heightError * data.heightAdjustSpeed), ForceMode.Acceleration);
            }
        }
    }
}