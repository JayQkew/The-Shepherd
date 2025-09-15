using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace HerdingSystem
{
    public class HerdArea : MonoBehaviour
    {
        public Destination destination;
        public Animal canHost;
        public List<HerdAnimal> animalsIn = new List<HerdAnimal>();
        public Dictionary<Animal, List<HerdAnimal>> animalsByType = new Dictionary<Animal, List<HerdAnimal>>();

        private void OnTriggerEnter(Collider other) {
            HerdAnimal animal = other.GetComponent<HerdAnimal>();
            if (animal) {
                animalsIn.Add(other.GetComponent<HerdAnimal>());
                animal.currHerdArea = destination;
                if (!animalsByType.ContainsKey(animal.animal)) {
                    animalsByType[animal.animal] = new List<HerdAnimal>();
                }
                animalsByType[animal.animal].Add(animal);
                HerdManager.Instance.CheckMissions(this);
            }
        }

        private void OnTriggerExit(Collider other) {
            HerdAnimal animal = other.GetComponent<HerdAnimal>();
            if (animal) {
                animalsIn.Remove(animal);
                animal.currHerdArea = Destination.None;
                if (animalsByType.TryGetValue(animal.animal, out var list)) {
                    list.Remove(animal);
                }
                HerdManager.Instance.CheckMissions(this);
            }
        }
    }
}
