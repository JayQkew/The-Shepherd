using System.Collections.Generic;
using UnityEngine;

namespace HerdingSystem
{
    public class HerdManager : MonoBehaviour
    {
        public static HerdManager Instance { get; private set; }
    
        public List<HerdMission> missions;
        public List<HerdDestination> destinations = new List<HerdDestination>();
        public List<HerdAnimal> allHerdAnimals = new List<HerdAnimal>();
        public HerdDestination pen;

        private Dictionary<Animal, List<HerdAnimal>> animalsByType = new Dictionary<Animal, List<HerdAnimal>>();

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
            else {
                Destroy(gameObject);
            }

            foreach (HerdDestination destination in destinations) {
                if (destination.destination == Destination.Pen) {
                    pen = destination;
                }
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

        public void UpdateMissions(HerdDestination herdDestination) {
            for (int i = 0; i < missions.Count; i++) {
                HerdMission herdMission = missions[i];
                if (herdMission.herdDestination == herdDestination) {
                    if(herdDestination.animalsByType.TryGetValue(herdMission.animal, out List<HerdAnimal> animals)){
                        herdMission.curr = animals.Count;
                    }
                }
                missions[i] = herdMission;
            }

            MissionGateControl(!AllMissionsComplete());
        }

        private bool AllMissionsComplete() {
            bool allMissionsComplete = true;

            foreach (var mission in missions) {
                allMissionsComplete &= mission.TargetMet;
            }
            
            return allMissionsComplete;
        }

        public void PenMission() {
            missions.Clear();
            HerdMission penMission = new HerdMission(
                pen,
                Animal.Sheep,
                animalsByType[Animal.Sheep].Count
            );
            missions.Add(penMission);
            pen.GetComponentInParent<HerdGate>().OpenGate();
        }

        public void GenerateMissions() {
            missions.Clear();
            // generate a mission for every animal type
            foreach (Animal animal in animalsByType.Keys) {
                List<HerdDestination> herdDestinations = AvailableDestinations(animal);
                int animals = animalsByType[animal].Count;
                
                int numMissions = Random.Range(1, herdDestinations.Count + 1);
                for (int i = 0; i < numMissions; i++) {
                    int target = 0;
                    if (i == numMissions - 1) {
                        target = animals;
                    }
                    else {
                        target = Random.Range(1, animals);
                        animals -= target;
                    }

                    HerdDestination herdDestination = herdDestinations[Random.Range(0, herdDestinations.Count)];
                    
                    HerdMission herdMission = new HerdMission(
                        herdDestination,
                        animal,
                        target);
                    
                    herdDestinations.Remove(herdDestination);
                    missions.Add(herdMission);
                }
            }
            
            MissionGateControl(true);
        }

        public void MissionGateControl(bool open) {
            foreach (HerdMission mission in missions) {
                if (open) {
                    mission.herdDestination.GetComponentInParent<HerdGate>().OpenGate();
                }
                else {
                    mission.herdDestination.GetComponentInParent<HerdGate>().CloseGate();
                }
            }
        }
        
        private List<HerdDestination> AvailableDestinations(Animal animal) {
            List<HerdDestination> herdDestinations = new List<HerdDestination>();
            Debug.Log(destinations.Count);
            foreach (HerdDestination destination in destinations) {
                if (destination.canHost.HasFlag(animal)) {
                    herdDestinations.Add(destination);
                }
            }
            return herdDestinations;
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