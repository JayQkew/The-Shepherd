using Creatures;
using UnityEngine;

namespace HerdingSystem
{
    public class HerdAnimal : Creatures.Animal
    {
        public Destination currHerdArea;

        private void Awake() {
            rb = GetComponent<Rigidbody>();
        }

        private void Start() {
            base.Start();
            HerdManager.Instance.AddAnimal(this);
        }

        private void OnDisable() {
            HerdManager.Instance.RemoveAnimal(this);
        }
    }
}
