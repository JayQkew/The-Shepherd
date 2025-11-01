using System.Collections.Generic;
using OffScreenIndicator;
using UnityEngine;

namespace HerdingSystem
{
    public class HerdDestination : MonoBehaviour
    {
        public Destination destination;
        public List<HerdAnimal> animalsIn = new List<HerdAnimal>();

        public HerdGate herdGate;
        public HerdAssist herdAssist;
        public OsiTarget osiTarget;

        private void OnTriggerEnter(Collider other) {
            HerdAnimal animal = other.GetComponent<HerdAnimal>();
            if (animal) {
                if (animalsIn.Contains(animal)) return;
                animalsIn.Add(animal);
                animal.currHerdArea = destination;
                HerdManager.Instance.UpdateMissions(this);
            }
        }

        private void OnTriggerExit(Collider other) {
            HerdAnimal animal = other.GetComponent<HerdAnimal>();
            if (animal) {
                animalsIn.Remove(animal);
                animal.currHerdArea = Destination.None;
                HerdManager.Instance.UpdateMissions(this);
            }
        }
    }
}
