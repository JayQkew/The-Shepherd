using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Notifications;
using TimeSystem;
using Unity.VisualScripting;
using UnityEditor;
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
        [SerializeField, Space(25)] SheepSpawn sheepSpawn;

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
                if (destination.destination == Destination.Barn) {
                    pen = destination;
                }
            }

            ticketManager.Init();
            sheepSpawn.SpawnSheep(3);
        }

        private void Start() {
            TimeManager.Instance.dayPhases[^1].onPhaseEnd.AddListener(sheepSpawn.SpawnSheep);
        }

        private void OnDestroy() {
            sheepSpawn.ClearSheepCount();
            TimeManager.Instance.dayPhases[^1].onPhaseEnd.RemoveListener(sheepSpawn.SpawnSheep);
        }

        public void AddAnimal(HerdAnimal herdAnimal) {
            if (herdAnimal == null || allHerdAnimals.Contains(herdAnimal)) return;
            allHerdAnimals.Add(herdAnimal);
        }

        public void RemoveAnimal(HerdAnimal herdAnimal) {
            if (herdAnimal == null) return;
            allHerdAnimals.Remove(herdAnimal);
        }

        public void UpdateMissions(HerdDestination herdDestination) {
            for (int i = 0; i < missions.Count; i++) {
                HerdMission herdMission = missions[i];
                if (herdMission.herdDestination == herdDestination) {
                    herdMission.curr = herdMission.herdDestination.animalsIn.Count;
                    missionUI.missionCards[i].UpdateNumbers();

                    HerdAssist.HerdDirection herdDirection = herdMission.TargetMet
                        ? HerdAssist.HerdDirection.Out
                        : HerdAssist.HerdDirection.In;

                    herdDestination.herdAssist.direction = herdDirection;
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
                allHerdAnimals.Count
            );
            missions.Add(penMission);
            missionUI.AddMissionCard(penMission);

            Notification notification = new Notification(
                "Herding",
                $"Herd {allHerdAnimals.Count} Sheep to {pen.destination.StringValue()}",
                5);

            MissionGateControl(true, HerdAssist.HerdDirection.In);
            AreasWithAnimalsGateControl(true, HerdAssist.HerdDirection.Out);
        }

        public void GenerateMissions() {
            missions.Clear();
            missionUI.RemoveAllMissionCards();

            List<HerdDestination> allDestinations = destinations.ToList();

            HerdingTicket ticket = ticketManager.GetTicket();

            List<HerdDestination> herdDestinations = destinations.ToList();
            int totalAnimals = allHerdAnimals.Count;

            int numMissions = ticket.weights.Count;
            int animalSum = 0;
            for (int i = 0; i < numMissions; i++) {
                HerdDestination herdDestination = herdDestinations[Random.Range(0, herdDestinations.Count)];
                int numAnimals = Mathf.RoundToInt(totalAnimals * ticket.weights[i]);
                animalSum += numAnimals;

                HerdMission herdMission = new HerdMission(herdDestination, numAnimals);

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
                    $"Herd {mission.target} Sheep to {mission.destination.StringValue()}",
                    10);
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

        private void OnDrawGizmos() {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(sheepSpawn.spawnPoint.position, sheepSpawn.spawnRadius);
        }
    }

    public enum Destination
    {
        None,
        [Description("the Barn")]
        Barn,
        [Description("the Northern Pasture")]
        NorthernPasture,
        [Description("the Western Pasture")]
        WesternPasture,
    }
}