using Creatures;
using UnityEngine;

namespace HerdingSystem
{
    public class HerdAnimal : Creatures.Animal
    {
        [Header("Herd Animal")]
        public Destination currHerdArea;

        protected override void Awake() {
            base.Awake();
            rb = GetComponent<Rigidbody>();
        }

        protected override void Start() {
            base.Start();
            HerdManager.Instance.AddAnimal(this);
        }

        private void OnDisable() {
            HerdManager.Instance.RemoveAnimal(this);
        }
    }
}
