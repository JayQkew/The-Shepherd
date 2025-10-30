using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Creatures
{
    [Serializable]
    public class BugFly : BugBaseState
    {
        private Rigidbody rb;
        private Vector3 targetDirection;
        private float noiseSeed;
        private float t;

        public override void EnterState(BugStateManager manager) {
            if (rb == null) rb = manager.rb;
            rb.useGravity = false;
            noiseSeed = Random.value * 100f;
            targetDirection = manager.transform.forward;
            manager.gui.PlayAnim("Fly");
            Debug.Log("Playing Fly Anim");
        }

        public override void UpdateState(BugStateManager manager) {
            if (rb == null) return;
            t = Time.time;

            Vector3 wanderDir = manager.WanderDirection(0.5f);
            targetDirection = Vector3.Lerp(targetDirection, wanderDir, Time.deltaTime * manager.bugData.flyTurnSpeed);
            Hover(manager);
            float flutter = Mathf.Sin(t * manager.bugData.flutterFrequency) * manager.bugData.flutterAmplitude;

            Vector3 horizontalVelocity = targetDirection * manager.bugData.flySpeed;
            Vector3 currentVelocity = rb.linearVelocity;
            Vector3 targetVelocity = new Vector3(
                horizontalVelocity.x,
                currentVelocity.y + flutter, 
                horizontalVelocity.z);

            rb.linearVelocity = Vector3.Lerp(currentVelocity, targetVelocity, Time.deltaTime * 3f);
        }

        public override void ExitState(BugStateManager manager) {
        }

        private void Hover(BugStateManager manager) {
            float currentHeight = manager.transform.position.y;
            bool foundGround = Physics.Raycast(manager.transform.position, Vector3.down, out RaycastHit hit,
                manager.bugData.raycastDistance, manager.bugData.groundMask);

            if (foundGround) {
                var groundHeight = hit.point.y;
                float desiredHeight = groundHeight + manager.bugData.targetHeight;
                float heightError = desiredHeight - currentHeight;

                rb.AddForce(Vector3.up * (heightError * manager.bugData.heightAdjustSpeed), ForceMode.Acceleration);
            }
        }
    }
}