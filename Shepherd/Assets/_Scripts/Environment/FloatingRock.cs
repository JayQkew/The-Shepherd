using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Environment
{
    [RequireComponent(typeof(Rigidbody))]
    public class FloatingRock : MonoBehaviour, IBarkable
    {
        private Rigidbody rb;
        private Vector3 targetPos;
        [SerializeField] private float angularDamping = 0.2f;
        [SerializeField] private float linearDamping = 1f;
        [SerializeField] private float returnForce = 2.5f;
        private Vector3 returnDir;

        private void Awake() {
            rb = GetComponent<Rigidbody>();
            
            rb.useGravity = false;
            rb.linearDamping = linearDamping;
            rb.angularDamping = angularDamping;
        }

        private void Start() {
            targetPos = transform.position;
        }

        private void Update() {
            float xDistance = transform.position.x - targetPos.x;
            float yDistance = transform.position.y - targetPos.y;
            float zDistance = transform.position.z - targetPos.z;
            
            returnDir = new Vector3(xDistance, yDistance, zDistance) * -1;
            
            rb.angularVelocity = Vector3.ClampMagnitude(rb.angularVelocity, 1);
        }

        private void FixedUpdate() {
            rb.AddForce(returnDir.normalized * returnForce, ForceMode.Acceleration);
            rb.AddTorque(transform.forward * returnForce, ForceMode.Force);
        }

        public void BarkedAt(Vector3 sourcePosition) {
            Vector3 dir = (transform.position - sourcePosition).normalized;
            rb.AddForce(dir * returnForce, ForceMode.Impulse);
        }
    }
}
