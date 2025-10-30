using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Notifications;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace HerdingSystem
{
    public class HerdManager : MonoBehaviour
    {
        public static HerdManager Instance { get; private set; }
        private MissionUI missionUI;

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
            missionUI = MissionUI.Instance;

            // finds the pen
            foreach (HerdDestination destination in destinations) {
                if (destination.destination == Destination.Pen) {
                    pen = destination;
                }
            }

            // puts tickets into dictionary
            ticketManager.Init();
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
                    if (herdDestination.animalsByType.TryGetValue(herdMission.animal, out List<HerdAnimal> animals)) {
                        herdMission.curr = animals.Count;
                        missionUI.missionCards[i].UpdateNumbers();

                        HerdAssist.HerdDirection herdDirection = herdMission.TargetMet
                            ? HerdAssist.HerdDirection.Out
                            : HerdAssist.HerdDirection.In;

                        herdDestination.herdAssist.direction = herdDirection;
                    }
                }

                missions[i] = herdMission;
            }

            if (AllMissionsComplete()) {
                MissionGateControl(false, HerdAssist.HerdDirection.None);
                GateControl(false, HerdAssist.HerdDirection.None);
                Notification notification = new Notification(
                    "Herding",
                    "All herding missions complete",
                    5);
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
            missionUI.RemoveAllMissionCards();

            HerdMission penMission = new HerdMission(
                pen,
                Animal.Sheep,
                animalsByType[Animal.Sheep].Count
            );
            missions.Add(penMission);
            missionUI.AddMissionCard(penMission);

            Notification notification = new Notification(
                "Herding",
                $"Herd {animalsByType[Animal.Sheep].Count} {Animal.Sheep.StringValue()} to {pen.destination.StringValue()}",
                5);

            MissionGateControl(true, HerdAssist.HerdDirection.In);
            AreasWithAnimalsGateControl(true, HerdAssist.HerdDirection.Out);
        }

        public void GenerateMissions() {
            missions.Clear();
            missionUI.RemoveAllMissionCards();

            List<HerdDestination> allDestinations = destinations.ToList();

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

                    allDestinations.Remove(herdDestination);
                    herdDestinations.Remove(herdDestination);
                    missions.Add(herdMission);
                }

                // this is to account for any rounding errors
                int diff = totalAnimals - animalSum;
                missions[numMissions - 1].target += diff;

                foreach (HerdMission mission in missions) {
                    missionUI.AddMissionCard(mission);
                    Notification notification = new Notification(
                        "Herding",
                        $"Herd {mission.target} {animal.StringValue()} to {mission.destination.StringValue()}",
                        10);
                }
            }

            foreach (HerdDestination destination in allDestinations) {
                destination.herdAssist.direction = HerdAssist.HerdDirection.None;
            }

            MissionGateControl(true, HerdAssist.HerdDirection.In);
            AreasWithAnimalsGateControl(true, HerdAssist.HerdDirection.Out);
        }

        #region Gate Control

        private void MissionGateControl(bool open, HerdAssist.HerdDirection assitDir) {
            foreach (HerdMission mission in missions) {
                if (open && assitDir == HerdAssist.HerdDirection.In) {
                    mission.herdDestination.osiTarget.Subscribe();
                } else if (!open && assitDir == HerdAssist.HerdDirection.None) {
                    mission.herdDestination.osiTarget.Unsubscribe();
                }

                mission.herdDestination.herdGate.GateControl(open);
                mission.herdDestination.herdAssist.direction = assitDir;
            }
        }

        private void AreasWithAnimalsGateControl(bool open, HerdAssist.HerdDirection assitDir) {
            foreach (HerdDestination destination in destinations) {
                if (destination.animalsIn.Count > 0) {
                    destination.herdGate.GateControl(open);
                    destination.herdAssist.direction = assitDir;
                }
            }
        }

        private void GateControl(bool open, HerdAssist.HerdDirection assitDir) {
            foreach (HerdDestination destination in destinations) {
                destination.herdGate.GateControl(open);
                destination.herdAssist.direction = assitDir;
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
        Sheep = 1 << 0,

        [Description("Ducken")]
        Ducken = 1 << 1
    }
}