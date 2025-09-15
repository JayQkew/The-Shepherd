using System.Collections.Generic;
using UnityEngine;

namespace HerdingSystem
{
    public class HerdManager : MonoBehaviour
    {
        public static HerdManager Instance { get; private set; }
    
        public List<HerdMission> missions;
    
        public List<HerdAnimal> allHerdAnimals = new List<HerdAnimal>();

        private Dictionary<Animal, List<HerdAnimal>> animalsByType = new Dictionary<Animal, List<HerdAnimal>>();

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
            else {
                Destroy(gameObject);
            }
        }

        public void AddAnimal(HerdAnimal herdAnimal) {
            if (herdAnimal == null || allHerdAnimals.Contains(herdAnimal)) return;
    
            allHerdAnimals.Add(herdAnimal);

            if (!animalsByType.ContainsKey(herdAnimal.animal)) {
                animalsByType[herdAnimal.animal] = new List<HerdAnimal>();
            }
        
            animalsByType[herdAnimal.animal].Add(herdAnimal);
        }

        public void RemoveAnimal(HerdAnimal herdAnimal) {
            if (herdAnimal == null) return;

            allHerdAnimals.Remove(herdAnimal);

            if (animalsByType.TryGetValue(herdAnimal.animal, out var list)) {
                list.Remove(herdAnimal);
            }
        }

        public void CheckMissions(HerdArea herdArea) {
            for (int i = 0; i < missions.Count; i++) {
                HerdMission herdMission = missions[i];
                if (herdMission.destination == herdArea.destination) {
                    if(herdArea.animalsByType.TryGetValue(herdMission.animal, out List<HerdAnimal> animals)){
                        herdMission.curr = animals.Count;
                    }
                }
                missions[i] = herdMission;
            }
        }

        public void GenerateMissions() {
            // generate a mission for every animal type
            foreach (Animal animal in animalsByType.Keys) {
                
            }
            
            // how many herd areas are there?
            // how many herd creatures are there?
            int destinations = 2;
            int animals = allHerdAnimals.Count;
            
            // how many missions do you want?
            int numMissions = Random.Range(1, destinations + 1);
            // generate a mission for each area
            
            // select an animal

            for (int i = 0; i < numMissions; i++) {
                int target = Random.Range(0, animals);
            }
            
        }
    }

    public enum Destination
    {
        None,
        Pen,
        Field
    }

    [System.Flags]
    public enum Animal
    {
        None = 0,
        Sheep   = 1 << 0,
        Ducken  = 1 << 1
    }
}