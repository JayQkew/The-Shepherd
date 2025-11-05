using System;
using UnityEngine;
using UnityEngine.Events;

namespace Creatures.Sheep
{
    [Serializable]
    public class Explosion
    {
        private SheepData sheepData;
        private Transform transform;
        private SphereCollider col;
        [SerializeField] private UnityEvent onExplode;

        public void Init(Transform trans, SphereCollider c, SheepData data) {
            transform = trans;
            col = c;
            sheepData = data;
        }
        
        public void PuffExplosion() {
            Vector3 origin = transform.position + new Vector3(0, -col.radius, 0);
            Collider[] cols = Physics.OverlapSphere(origin, sheepData.explosionRadius);
            if (cols.Length == 0) return;
    
            foreach (Collider c in cols) {
                IBarkable barkable = c.GetComponent<IBarkable>();
                if (barkable != null) {
                    barkable.BarkedAt(transform);
                }
                else {
                    Rigidbody targetRb = c.GetComponent<Rigidbody>();
                    if (targetRb != null) {
                        Vector3 dir = (c.transform.position - origin).normalized;
                        float distance = Vector3.Distance(origin, targetRb.transform.position);
                        float fallOff = Mathf.Clamp01(1 - distance / sheepData.explosionRadius);
    
                        float force = sheepData.explosionForce * fallOff;
                        targetRb.AddForce(dir * force, ForceMode.Impulse);
                    }
                }
            }
            
            onExplode?.Invoke();
        }
    }
}
