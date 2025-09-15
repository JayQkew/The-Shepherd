using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.HerdingSystem
{
    public class HerdManager : MonoBehaviour
    {
        public static HerdManager Instance { get; private set; }
    
        public List<HerdTarget> targets;
    
        public List<HerdAnimal> allHerdAnimals = new List<HerdAnimal>();

        private Dictionary<AnimalName, List<HerdAnimal>> animalsByType = new Dictionary<AnimalName, List<HerdAnimal>>();

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
            else {
                Destroy(gameObject);
            }
        }

        public void AddHerdAnimal(HerdAnimal herdAnimal) {
            if (herdAnimal == null || allHerdAnimals.Contains(herdAnimal)) return;
    
            allHerdAnimals.Add(herdAnimal);

            if (!animalsByType.ContainsKey(herdAnimal.animalName)) {
                animalsByType[herdAnimal.animalName] = new List<HerdAnimal>();
            }
        
            animalsByType[herdAnimal.animalName].Add(herdAnimal);
        }

        public void RemoveHerdAnimal(HerdAnimal herdAnimal) {
            if (herdAnimal == null) return;

            allHerdAnimals.Remove(herdAnimal);

            if (animalsByType.TryGetValue(herdAnimal.animalName, out var list)) {
                list.Remove(herdAnimal);
            }
        }

        public void CheckTargets(HerdArea herdArea) {
            for (int i = 0; i < targets.Count; i++) {
                HerdTarget herdTarget = targets[i];
                if (herdTarget.destination == herdArea.areaName) {
                    if(herdArea.animalsByType.TryGetValue(herdTarget.animal, out List<HerdAnimal> animals)){
                        herdTarget.curr = animals.Count;
                    }
                }
                targets[i] = herdTarget;
            }
        }
    }

    public enum HerdAreaName
    {
        None,
        Pen,
        Field
    }

    public enum AnimalName
    {
        Sheep,
        Ducken
    }
}