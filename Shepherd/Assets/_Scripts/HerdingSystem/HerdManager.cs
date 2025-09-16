using System;
using System.Collections.Generic;
using System.Linq;
using TimeSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HerdingSystem
{
    public class HerdManager : MonoBehaviour
    {
        public static HerdManager Instance { get; private set; }
    
        public List<HerdMission> missions;
        public List<HerdDestination> destinations = new List<HerdDestination>();
        public List<HerdAnimal> allHerdAnimals = new List<HerdAnimal>();
        private HerdDestination pen;
        [SerializeField] private List<HerdingTicket> herdingTickets = new List<HerdingTicket>();
        
        private Dictionary<TicketDifficulty, List<HerdingTicket>> ticketDifficulties = new Dictionary<TicketDifficulty, List<HerdingTicket>>
        {
            { TicketDifficulty.Easy , new List<HerdingTicket>()},
            { TicketDifficulty.Medium , new List<HerdingTicket>()},
            { TicketDifficulty.Hard , new List<HerdingTicket>()},
        };

        private Dictionary<Animal, List<HerdAnimal>> animalsByType = new Dictionary<Animal, List<HerdAnimal>>();

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
            else {
                Destroy(gameObject);
            }

            // finds the pen
            foreach (HerdDestination destination in destinations) {
                if (destination.destination == Destination.Pen) {
                    pen = destination;
                }
            }
            
            // puts tickets into dictionary
            foreach (HerdingTicket ticket in herdingTickets) {
                ticketDifficulties[ticket.difficulty].Add(ticket);
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
            // generate missions for every animal type
            foreach (Animal animal in animalsByType.Keys) {
                HerdingTicket ticket = GetHerdingTicket();
                
                List<HerdDestination> herdDestinations = AvailableDestinations(animal);
                int totalAnimals = animalsByType[animal].Count;
                
                int numMissions = ticket.weights.Count;
                for (int i = 0; i < numMissions; i++) {
                    HerdDestination herdDestination = herdDestinations[Random.Range(0, herdDestinations.Count)];
                    int numAnimals = Mathf.RoundToInt(totalAnimals / ticket.weights[i]);
                    
                    HerdMission herdMission = new HerdMission(
                        herdDestination,
                        animal,
                        numAnimals);
                    
                    herdDestinations.Remove(herdDestination);
                    missions.Add(herdMission);
                }
            }
            
            MissionGateControl(true);
        }

        /// <summary>
        /// Gets a herding ticket based on how many days the player has played
        /// 0 - 5 days = Easy
        /// 6 - 17 days = Easy + Medium
        /// 18 - ∞ = Easy + Medium + Hard
        /// </summary>
        private HerdingTicket GetHerdingTicket() {
            uint day = TimeManager.Instance.dayCount;
            HerdingTicket ticket;

            if (day >= 18) {
                List<HerdingTicket> tickets = ticketDifficulties[TicketDifficulty.Easy]
                    .Concat(ticketDifficulties[TicketDifficulty.Medium])
                    .Concat(ticketDifficulties[TicketDifficulty.Hard])
                    .ToList();
                int i = Random.Range(0, tickets.Count);
                ticket = tickets[i];
            } 
            else if (day >= 6) {
                List<HerdingTicket> tickets = ticketDifficulties[TicketDifficulty.Easy]
                    .Concat(ticketDifficulties[TicketDifficulty.Medium])
                    .ToList();
                int i = Random.Range(0, tickets.Count);
                ticket = tickets[i];
            }
            else {
                int i = Random.Range(0, ticketDifficulties[TicketDifficulty.Easy].Count);
                ticket = ticketDifficulties[TicketDifficulty.Easy][i];
            }
            
            return ticket;
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

            herdDestinations.Remove(pen); // ensures that the pen is never chosen
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