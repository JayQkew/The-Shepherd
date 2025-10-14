using UnityEngine;

namespace Environment
{
    [RequireComponent(typeof(Rigidbody))]
    public class FloatingRock : MonoBehaviour
    {
        [Header("Floating")]
        public float floatHeight = 5f;          // Target height to float at
        public float floatForce = 50f;          // Upward force to maintain height
        public float floatDamping = 0.5f;       // Damping to smooth movement
    
        [Header("Interaction")]
        public float pushForce = 100f;          // Force applied when pushed
        public float dragDamping = 0.3f;        // How quickly velocity decays
    
        private Rigidbody rb;
        private Vector3 startPosition;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            startPosition = transform.position;
        
            // Make sure Rigidbody is set up correctly
            floatHeight = transform.position.y;
            rb.useGravity = false;
            // rb.constraints = RigidbodyConstraints.None;
        }

        void FixedUpdate()
        {
            // Apply floating force - keeps rock at target height
            float heightDifference = floatHeight - (transform.position.y - startPosition.y);
            float upwardForce = heightDifference * floatForce;
        
            // Apply damping to smooth the floating
            Vector3 velocityDamping = -rb.linearVelocity * floatDamping;
        
            rb.AddForce(Vector3.up * upwardForce + velocityDamping, ForceMode.Acceleration);
        
            // Apply general drag to all velocity
            rb.linearVelocity *= (1f - dragDamping * Time.fixedDeltaTime);
        }

        // Call this when something hits the rock (use OnCollisionEnter or raycasts)
        public void PushRock(Vector3 pushDirection)
        {
            rb.AddForce(pushDirection.normalized * pushForce, ForceMode.Impulse);
        }

        // Alternative: push from a specific point
        public void PushRockFromPoint(Vector3 hitPoint, Vector3 direction)
        {
            rb.AddForceAtPosition(direction.normalized * pushForce, hitPoint, ForceMode.Impulse);
        }
    }
}
