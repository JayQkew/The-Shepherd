using UnityEngine;

namespace HerdingSystem
{
    public class HerdAssist : MonoBehaviour
    {
        private Collider col;
        [SerializeField] private HerdDirection direction;
        [Space(10)]
        [SerializeField] private float centerMult;
        [SerializeField] private float pushMult;

        [Space(10)]
        [SerializeField] private float pushZone;

        private void PushDirection(Rigidbody rb, Vector3 dir, float forceMult) {
            Vector3 localForce = dir.normalized;
            Vector3 worldForce = transform.TransformDirection(localForce);
            rb.AddForce(worldForce * forceMult, ForceMode.Impulse);
        }

        private bool InPushZone(Vector3 worldPos) {
            Vector3 localPos = transform.InverseTransformPoint(worldPos);
            return localPos.x < pushZone && localPos.x > -pushZone;
        }

        private void OnTriggerStay(Collider other) {
            HerdAnimal h = other.gameObject.GetComponent<HerdAnimal>();
            if (h != null && direction != HerdDirection.None) {
                Vector3 localPos = transform.InverseTransformPoint(h.transform.position);
                PushDirection(h.rb, new Vector3(-localPos.x, 0, 0), centerMult);
                if (InPushZone(h.transform.position)) {
                    if(direction == HerdDirection.In) PushDirection(h.rb, -transform.forward, pushMult);
                    else PushDirection(h.rb, transform.forward, pushMult);
                }
            }
        }

        private void OnDrawGizmos() {
            BoxCollider box = GetComponent<BoxCollider>();
            if (box == null) return;

            Gizmos.color = Color.red;

            // Save the old matrix
            var old = Gizmos.matrix;

            // Align the gizmo with the object's transform
            Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);

            // Build the size: pushZone along X, collider's size for Y/Z
            Vector3 size = new Vector3(
                2f * pushZone,                         // X = pushZone width
                box.size.y * transform.lossyScale.y,   // Y = collider height in world
                box.size.z * transform.lossyScale.z    // Z = collider depth in world
            );

            // Draw a wire cube at the collider's center
            Gizmos.DrawWireCube(box.center, size);

            // Restore
            Gizmos.matrix = old;
        }

        public enum HerdDirection
        {
            None,
            In,
            Out
        }
    }
}
