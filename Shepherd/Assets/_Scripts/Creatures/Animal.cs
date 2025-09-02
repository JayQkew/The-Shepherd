using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Creatures
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(SphereCollider))]
    public class Animal : MonoBehaviour
    {
        [SerializeField] protected AnimalData AnimalData;
        public string animalName;
        [Space(20)]
        public Rigidbody rb;
        public SphereCollider col;
    
        protected void Awake() {
            rb = GetComponent<Rigidbody>();
            col = GetComponent<SphereCollider>();
        }

        private void Start() {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.mass = AnimalData.mass.RandomValue();
            rb.useGravity = AnimalData.useGravity;
            rb.linearDamping = AnimalData.linearDamping;
        }

        public override string ToString() {
            return $"{base.ToString()} : [{animalName}]";
        }
    }
}

