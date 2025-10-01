using System.Collections.Generic;
using OffScreenIndicator;
using UnityEngine;

namespace HerdingSystem
{
    public class HerdDestination : MonoBehaviour
    {
        public Destination destination;
        public Animal canHost;
        public List<HerdAnimal> animalsIn = new List<HerdAnimal>();
        public Dictionary<Animal, List<HerdAnimal>> animalsByType = new Dictionary<Animal, List<HerdAnimal>>();

        public HerdGate herdGate;
        public HerdAssist herdAssist;
        public OsiTarget osiTarget;

        private void OnTriggerEnter(Collider other) {
            HerdAnimal animal = other.GetComponent<HerdAnimal>();
            if (animal) {
                if (animalsIn.Contains(animal)) return;
                animalsIn.Add(animal);
                
                animal.currHerdArea = destination;
                if (!animalsByType.ContainsKey(animal.animal)) {
                    animalsByType[animal.animal] = new List<HerdAnimal>();
                }

                if (animalsByType[animal.animal].Contains(animal)) return;
                animalsByType[animal.animal].Add(animal);
                
                HerdManager.Instance.UpdateMissions(this);
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
                HerdManager.Instance.UpdateMissions(this);
            }
        }
    }
}
