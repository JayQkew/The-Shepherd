using Audio;
using FMODUnity;
using HerdingSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Creatures
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(SphereCollider))]
    [RequireComponent(typeof(StudioEventEmitter))]
    public class Animal : MonoBehaviour
    {
        [Header("Animal")]
        [Space(10)]
        [SerializeField] protected AnimalData animalData;
        public HerdingSystem.Animal animal;
        [HideInInspector] public Rigidbody rb;
        [HideInInspector] public SphereCollider col;
        [HideInInspector] public StudioEventEmitter emitter;
        [HideInInspector] public AudioManager audioManager;
        [HideInInspector] public FMODEvents fmodEvents;
    
        protected virtual void Awake() {
            rb = GetComponent<Rigidbody>();
            col = GetComponent<SphereCollider>();
        }

        protected virtual void Start() {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.mass = animalData.mass.RandomValue();
            rb.useGravity = animalData.useGravity;
            rb.linearDamping = animalData.linearDamping;
            
            audioManager = AudioManager.Instance;
            fmodEvents = FMODEvents.Instance;
            emitter = audioManager.InitializeEventEmitter(gameObject);
        }

        public override string ToString() {
            return $"{base.ToString()} : [{animal.ToString()}]";
        }
    }
}

