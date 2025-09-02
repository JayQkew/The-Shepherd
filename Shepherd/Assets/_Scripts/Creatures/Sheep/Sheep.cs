using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace _Scripts.Creatures.Sheep
{
    public class Sheep : Animal, IBarkable
    {
        private SheepStateManager sheepStateManager;
        [SerializeField] private float barkForce;

        [Space(20)]
        [SerializeField] private Wool wool;
    
        [Header("Explosion")]
        [SerializeField] private float radius;
        [SerializeField] private float forceMult;
        [SerializeField] private UnityEvent onExplode;
        [SerializeField]private bool showGizmos;
    
        private void Awake() {
            base.Awake();
            sheepStateManager = GetComponent<SheepStateManager>();
        }
    
        public void BarkedAt(Vector3 sourcePosition) {
            Vector3 dir = (transform.position - sourcePosition).normalized;
            rb.AddForce(dir * barkForce, ForceMode.Impulse);
            sheepStateManager.SwitchState(sheepStateManager.sheepRun);
            Debug.Log("Barked At");
        }
    
        private void Update() {
            wool.WoolUpdate();
            rb.mass = AnimalData.mass.Lerp(wool.woolValue);
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
    
        private void OnDrawGizmos() {
            if (showGizmos) {
                col = GetComponent<SphereCollider>();
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(transform.position + new Vector3(0, -col.radius, 0), radius);
            }
        }
    }
}
