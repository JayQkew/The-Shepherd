using UnityEngine;

namespace _Scripts.Creatures
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(SphereCollider))]
    public class Animal : MonoBehaviour
    {
        public Rigidbody rb;
        public SphereCollider col;
    
        private void Awake() {
            rb = GetComponent<Rigidbody>();
            col = GetComponent<SphereCollider>();
        }
    }
}

