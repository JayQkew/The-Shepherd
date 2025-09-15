using _Scripts.Creatures;
using _Scripts.Creatures.Sheep;
using HerdingSystem;
using UnityEngine;

namespace Creatures.Sheep
{
    public class Sheep : HerdAnimal, IBarkable
    {
        private SheepStateManager sheepStateManager;
        [SerializeField] private float barkForce;
        [Space(20)]
        public Food food;
        [Space(20)]
        [SerializeField] private Wool wool;
        [Space(20)]
        [SerializeField] private Explosion explosion;
    
        private void Awake() {
            base.Awake();
            sheepStateManager = GetComponent<SheepStateManager>();
            wool.Init(sheepStateManager.stats.woolTime);
            explosion.Init(transform, col);
        }
    
        public void BarkedAt(Vector3 sourcePosition) {
            Vector3 dir = (transform.position - sourcePosition).normalized;
            rb.AddForce(dir * barkForce, ForceMode.Impulse);
            sheepStateManager.SwitchState(sheepStateManager.sheepRun);
            Debug.Log("Barked At");
        }
    
        private void Update() {
            wool.WoolUpdate();
            rb.mass = animalData.mass.Lerp(wool.woolValue);
        }

        public void PuffExplosion() => explosion.PuffExplosion();
    }
}
