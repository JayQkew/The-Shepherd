using Boids;
using HerdingSystem;
using UnityEngine;
using Utilities;

namespace Creatures.Sheep
{
    public class Sheep : HerdAnimal, IBarkable
    {
        [Space(25)]
        [Header("Sheep")]
        [Space(10)]
        [SerializeField] private float barkForce;
        [SerializeField] private Chance mehChance;
        public Food food;
        [SerializeField] private Wool wool;
        [SerializeField] private Explosion explosion;
        [HideInInspector] public Boid boid;
        [HideInInspector] public SheepData sheepData;
        public SheepGUI gui;
        
        
        protected override void Awake() {
            base.Awake();
            sheepData = data as SheepData;

            if (sheepData == null) {
                Debug.LogWarning("animal data not sheep stats");
                return;
            }
            
            wool.Init(sheepData.woolTime);
            explosion.Init(transform, col);
            gui = GetComponent<SheepGUI>();
            boid = GetComponent<Boid>();
        }

        public virtual void BarkedAt(Transform source) {
            Vector3 dir = (transform.position - source.position).normalized;
            rb.AddForce(dir * barkForce, ForceMode.Impulse);

            if (mehChance.Roll()) {
                PlayMeh();
            }
        }
    
        protected virtual void Update() {
            wool.WoolUpdate();
            rb.mass = sheepData.mass.Lerp(wool.woolValue);
        }

        public void PuffExplosion() {
            explosion.PuffExplosion();
            emitter.EventReference = fmodEvents.sheepPoof;
            emitter.Play();
        }

        public void PlayMeh() {
            emitter.EventReference = fmodEvents.sheepMeh;
            emitter.Play();
        }
    }
}
