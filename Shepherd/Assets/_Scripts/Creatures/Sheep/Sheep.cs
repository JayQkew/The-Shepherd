using System;
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
        public SheepData sheepData;
        public SheepGUI gui;
        
        public void Init(SheepData existingData = null) {
            if (existingData != null) sheepData = existingData;
            else if (sheepData == null) sheepData = Instantiate(data as SheepData);

            if (sheepData == null) {
                Debug.LogWarning("animal data not sheep stats");
                return;
            }

            wool.Init(sheepData);
            explosion.Init(transform, col, sheepData);
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
            rb.mass = sheepData.mass.Lerp(sheepData.woolTimer.Progress);
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

        private void OnDrawGizmos() {
            SheepData sData = data as SheepData;
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, sData.explosionRadius);
        }
    }
}