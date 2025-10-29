using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Creatures
{
    [Serializable]
    public class BugFly : BugBaseState
    {
        [Header("Movement Settings")]
        public float speed = 2f;
        public float turnSpeed = 2f;
        public float wanderStrength = 1f;

        [Header("Hover Settings")]
        public float targetHeight = 2f;          // Ideal height above ground
        public float heightAdjustSpeed = 2f;     // How quickly it adjusts its height
        public float raycastDistance = 10f;      // Max distance to detect ground
        public LayerMask groundMask;             // Optional: specify what counts as "ground"

        [Header("Flutter Settings")]
        public float flutterAmplitude = 0.5f;
        public float flutterFrequency = 5f;

        private Vector3 targetDirection;
        private float noiseSeed;

        public override void EnterState(BugStateManager manager)
        {
            noiseSeed = Random.value * 100f;
            targetDirection = manager.transform.forward;
        }

        public override void UpdateState(BugStateManager manager)
        {
            Rigidbody rb = manager.rb;
            if (rb == null) return;

            float t = Time.time;

            float noiseX = Mathf.PerlinNoise(noiseSeed, t * 0.5f) * 2f - 1f;
            float noiseZ = Mathf.PerlinNoise(noiseSeed + 10f, t * 0.5f) * 2f - 1f;
            Vector3 wanderDir = new Vector3(noiseX, 0, noiseZ).normalized;
            targetDirection = Vector3.Lerp(targetDirection, wanderDir, Time.deltaTime * turnSpeed);

            float currentHeight = manager.transform.position.y;
            float groundHeight = currentHeight - targetHeight;
            bool foundGround = Physics.Raycast(manager.transform.position, Vector3.down, out RaycastHit hit, raycastDistance, groundMask);

            if (foundGround)
            {
                groundHeight = hit.point.y;
                float desiredHeight = groundHeight + targetHeight;
                float heightError = desiredHeight - currentHeight;

                rb.AddForce(Vector3.up * (heightError * heightAdjustSpeed), ForceMode.Acceleration);
            }

            float flutter = Mathf.Sin(t * flutterFrequency) * flutterAmplitude;

            Vector3 horizontalVelocity = targetDirection * speed;
            Vector3 currentVelocity = rb.linearVelocity;
            Vector3 targetVelocity = new Vector3(horizontalVelocity.x, currentVelocity.y + flutter, horizontalVelocity.z);

            rb.linearVelocity = Vector3.Lerp(currentVelocity, targetVelocity, Time.deltaTime * 3f);
        }

        public override void ExitState(BugStateManager manager) { }
    }
}
