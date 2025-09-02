using System;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Creatures.Sheep
{
    [Serializable]
    public class Explosion
    {
        private Transform transform;
        private SphereCollider col;
        [SerializeField] private float radius;
        [SerializeField] private float forceMult;
        [SerializeField] private UnityEvent onExplode;

        public Explosion(Transform transform, SphereCollider col) {
            Init(transform, col);
        }

        public void Init(Transform trans, SphereCollider c) {
            transform = trans;
            col = c;
        }
        
        public void PuffExplosion() {
            Vector3 origin = transform.position + new Vector3(0, -col.radius, 0);
            Collider[] cols = Physics.OverlapSphere(origin, radius);
            if (cols.Length == 0) return;
    
            foreach (Collider c in cols) {
                IBarkable barkable = c.GetComponent<IBarkable>();
                if (barkable != null) {
                    barkable.BarkedAt(origin);
                }
                else {
                    Rigidbody targetRb = c.GetComponent<Rigidbody>();
                    if (targetRb != null) {
                        Vector3 dir = (c.transform.position - origin).normalized;
                        float distance = Vector3.Distance(origin, targetRb.transform.position);
                        float fallOff = Mathf.Clamp01(1 - distance / radius);
    
                        float force = forceMult * fallOff;
                        targetRb.AddForce(dir * force, ForceMode.Impulse);
                    }
                }
            }
            
            onExplode?.Invoke();
        }
    }
}
