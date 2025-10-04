using _Scripts.Creatures;
using _Scripts.Creatures.Sheep;
using Boids;
using HerdingSystem;
using UnityEngine;

namespace Creatures.Sheep
{
    public class Sheep : HerdAnimal, IBarkable
    {
        [Space(25)]
        [Header("Sheep")]
        [Space(10)]
        public SheepStats stats;
        [SerializeField] private float barkForce;
        public Food food;
        [SerializeField] private Wool wool;
        [SerializeField] private Explosion explosion;
        [HideInInspector] public Boid boid;
        public SheepGUI gui;

        protected override void Awake() {
            base.Awake();
            wool.Init(stats.woolTime);
            explosion.Init(transform, col);
            gui = GetComponent<SheepGUI>();
            boid = GetComponent<Boid>();
        }

        public virtual void BarkedAt(Vector3 sourcePosition) {
            Vector3 dir = (transform.position - sourcePosition).normalized;
            rb.AddForce(dir * barkForce, ForceMode.Impulse);
        }
    
        private void Update() {
            wool.WoolUpdate();
            rb.mass = animalData.mass.Lerp(wool.woolValue);
        }

        public void PuffExplosion() => explosion.PuffExplosion();
    }
}
