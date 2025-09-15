using System;
using _Scripts.HerdingSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Creatures
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(SphereCollider))]
    public class Animal : MonoBehaviour
    {
        [SerializeField] protected AnimalData animalData;
        public AnimalName animalName;
        public Rigidbody rb;
        public SphereCollider col;
    
        protected void Awake() {
            rb = GetComponent<Rigidbody>();
            col = GetComponent<SphereCollider>();
        }

        protected void Start() {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.mass = animalData.mass.RandomValue();
            rb.useGravity = animalData.useGravity;
            rb.linearDamping = animalData.linearDamping;
        }

        public override string ToString() {
            return $"{base.ToString()} : [{animalName.ToString()}]";
        }
    }
}

