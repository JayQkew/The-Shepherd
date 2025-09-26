using System.Collections.Generic;
using System.ComponentModel;
using Notifications;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace HerdingSystem
{
    public class HerdManager : MonoBehaviour
    {
        public static HerdManager Instance { get; private set; }
        private HerdUIManager herdUIManager;

        public List<HerdMission> missions;
        public HerdDestination[] destinations;
        public List<HerdAnimal> allHerdAnimals = new List<HerdAnimal>();
        private HerdDestination pen;
        
        [SerializeField, Space(25)] TicketManager ticketManager;

        private Dictionary<Animal, List<HerdAnimal>> animalsByType = new Dictionary<Animal, List<HerdAnimal>>();

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
            else {
                Destroy(gameObject);
            }
            
            destinations = FindObjectsByType<HerdDestination>(FindObjectsSortMode.None);
            herdUIManager = GetComponent<HerdUIManager>();

            // finds the pen
            foreach (HerdDestination destination in destinations) {
                if (destination.destination == Destination.Pen) {
                    pen = destination;
                }
            }
            
            // puts tickets into dictionary
            ticketManager.Init();
            // foreach (HerdingTicket ticket in herdingTickets) {
            //     ticketDifficulties[ticket.difficulty].Add(ticket);
            // }
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
                        herdUIManager.missionCards[i].UpdateNumbers();
                        
                        HerdAssist.HerdDirection herdDirection = herdMission.TargetMet ? 
                            HerdAssist.HerdDirection.Out : HerdAssist.HerdDirection.In;
                        
                        herdDestination.transform.parent.GetComponentInChildren<HerdAssist>().direction = herdDirection;
                    }
                }
                missions[i] = herdMission;
            }

            if (AllMissionsComplete()) {
                MissionGateControl(false, HerdAssist.HerdDirection.None);
            }
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
            herdUIManager.RemoveAllMissionCards();

            HerdMission penMission = new HerdMission(
                pen,
                Animal.Sheep,
                animalsByType[Animal.Sheep].Count
            );
            missions.Add(penMission);
            herdUIManager.AddMissionCard(penMission);
            
            Notification notification = new Notification(
                "Herding",
                $"Herd {animalsByType[Animal.Sheep].Count} {Animal.Sheep.StringValue()} to {pen.destination.StringValue()}",
                5);

            MissionGateControl(true, HerdAssist.HerdDirection.In);
            AreasWithAnimalsGateControl(true, HerdAssist.HerdDirection.Out);
        }

        public void GenerateMissions() {
            missions.Clear();
            herdUIManager.RemoveAllMissionCards();
            
            // generate missions for every animal type
            foreach (Animal animal in animalsByType.Keys) {
                HerdingTicket ticket = ticketManager.GetTicket();
                
                List<HerdDestination> herdDestinations = AvailableDestinations(animal);
                int totalAnimals = animalsByType[animal].Count;
                
                int numMissions = ticket.weights.Count;
                int animalSum = 0;
                for (int i = 0; i < numMissions; i++) {
                    HerdDestination herdDestination = herdDestinations[Random.Range(0, herdDestinations.Count)];
                    int numAnimals = Mathf.RoundToInt(totalAnimals * ticket.weights[i]);
                    animalSum += numAnimals;
                    
                    HerdMission herdMission = new HerdMission(
                        herdDestination,
                        animal,
                        numAnimals);
          
                    herdDestinations.Remove(herdDestination);
                    missions.Add(herdMission);


                }

                // this is to account for any rounding errors
                int diff = totalAnimals - animalSum;
                missions[numMissions - 1].target += diff;

                foreach (HerdMission mission in missions) {
                    herdUIManager.AddMissionCard(mission);
                    Notification notification = new Notification(
                        "Herding",
                        $"Herd {mission.target} {animal.StringValue()} to {mission.destination.StringValue()}",
                        5);
                }
            }
            
            MissionGateControl(true, HerdAssist.HerdDirection.In);
            AreasWithAnimalsGateControl(true, HerdAssist.HerdDirection.Out);
        }

        #region Gate Control
        private void MissionGateControl(bool open, HerdAssist.HerdDirection assitDir) {
            foreach (HerdMission mission in missions) {
                mission.herdDestination.GetComponentInParent<HerdGate>().GateControl(open);
                mission.herdDestination.transform.parent.GetComponentInChildren<HerdAssist>().direction = assitDir;
            }
        }

        private void AreasWithAnimalsGateControl(bool open, HerdAssist.HerdDirection assitDir) {
            foreach (HerdDestination destination in destinations) {
                if (destination.animalsIn.Count > 0) {
                    destination.GetComponentInParent<HerdGate>().GateControl(open);
                    destination.transform.parent.GetComponentInChildren<HerdAssist>().direction = assitDir;
                }
            }
        }
        #endregion
        
        private List<HerdDestination> AvailableDestinations(Animal animal) {
            List<HerdDestination> herdDestinations = new List<HerdDestination>();
            Debug.Log(destinations.Length);
            foreach (HerdDestination destination in destinations) {
                if (destination.canHost.HasFlag(animal)) {
                    herdDestinations.Add(destination);
                }
            }

            herdDestinations.Remove(pen); // ensures that the pen is never chosen
            return herdDestinations;
        }
    }

    public enum Destination
    {
        None,
        [Description("the Pen")]
        Pen,
        [Description("Field 1")]
        Field1,
        [Description("Field 2")]
        Field2,
        [Description("Field 3")]
        Field3,
        [Description("Field 4")]
        Field4,
        [Description("Field 5")]
        Field5,
    }

    [System.Flags]
    public enum Animal
    {
        None = 0,
        [Description("Sheep")]
        Sheep   = 1 << 0,        
        [Description("Ducken")]
        Ducken  = 1 << 1
    }
}