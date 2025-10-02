using HerdingSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Creatures
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(SphereCollider))]
    public class Animal : MonoBehaviour
    {
        [SerializeField] protected AnimalData animalData;
        [FormerlySerializedAs("animalName")] public HerdingSystem.Animal animal;
        public Rigidbody rb;
        public SphereCollider col;
    
        protected virtual void Awake() {
            rb = GetComponent<Rigidbody>();
            col = GetComponent<SphereCollider>();
        }

        protected virtual void Start() {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.mass = animalData.mass.RandomValue();
            rb.useGravity = animalData.useGravity;
            rb.linearDamping = animalData.linearDamping;
        }

        public override string ToString() {
            return $"{base.ToString()} : [{animal.ToString()}]";
        }
    }
}

