using Creatures;
using UnityEngine;

namespace HerdingSystem
{
    public class HerdAnimal : Animal
    {
        public HerdAreaName currHerdArea;

        private void Awake() {
            rb = GetComponent<Rigidbody>();
        }

        private void Start() {
            base.Start();
            HerdManager.Instance.AddHerdAnimal(this);
        }

        private void OnDisable() {
            HerdManager.Instance.RemoveHerdAnimal(this);
        }
    }
}
