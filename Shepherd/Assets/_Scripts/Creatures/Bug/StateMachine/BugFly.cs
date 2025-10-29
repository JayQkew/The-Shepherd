using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Creatures
{
    [Serializable]
    public class BugFly : BugBaseState
    {
        [SerializeField] private BugFlyData data;

        private Vector3 targetDirection;
        private float noiseSeed;

        public override void EnterState(BugStateManager manager) {
            manager.rb.useGravity = false;
            noiseSeed = Random.value * 100f;
            targetDirection = manager.transform.forward;
        }

        public override void UpdateState(BugStateManager manager) {
            Rigidbody rb = manager.rb;
            if (rb == null) return;

            float t = Time.time;

            float noiseX = Mathf.PerlinNoise(noiseSeed, t * 0.5f) * 2f - 1f;
            float noiseZ = Mathf.PerlinNoise(noiseSeed + 10f, t * 0.5f) * 2f - 1f;
            Vector3 wanderDir = new Vector3(noiseX, 0, noiseZ).normalized;
            targetDirection = Vector3.Lerp(targetDirection, wanderDir, Time.deltaTime * data.turnSpeed);

            float currentHeight = manager.transform.position.y;
            float groundHeight = currentHeight - data.targetHeight;
            bool foundGround = Physics.Raycast(manager.transform.position, Vector3.down, out RaycastHit hit,
                data.raycastDistance, data.groundMask);

            if (foundGround) {
                groundHeight = hit.point.y;
                float desiredHeight = groundHeight + data.targetHeight;
                float heightError = desiredHeight - currentHeight;

                rb.AddForce(Vector3.up * (heightError * data.heightAdjustSpeed), ForceMode.Acceleration);
            }

            float flutter = Mathf.Sin(t * data.flutterFrequency) * data.flutterAmplitude;

            Vector3 horizontalVelocity = targetDirection * data.speed;
            Vector3 currentVelocity = rb.linearVelocity;
            Vector3 targetVelocity =
                new Vector3(horizontalVelocity.x, currentVelocity.y + flutter, horizontalVelocity.z);

            rb.linearVelocity = Vector3.Lerp(currentVelocity, targetVelocity, Time.deltaTime * 3f);
        }

        public override void ExitState(BugStateManager manager) {
        }
    }
}