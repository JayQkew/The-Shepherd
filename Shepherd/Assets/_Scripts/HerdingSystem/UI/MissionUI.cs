using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace HerdingSystem
{
    public class MissionUI : MonoBehaviour
    {
        public static MissionUI Instance { get; private set; }
        [SerializeField] private GameObject missionCard;
        [SerializeField] private Transform missionCardContainer;
        public List<HerdMissionUI> missionCards = new List<HerdMissionUI>();

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
            else {
                Destroy(gameObject);
            }
        }

        public void AddMissionCard(HerdMission mission) {
            GameObject card = Instantiate(missionCard, missionCardContainer);
            HerdMissionUI missionUI = card.GetComponent<HerdMissionUI>();
            missionUI.Init(mission);
            missionCards.Add(missionUI);
        }
        
        public void RemoveMissionCard(GameObject card) {
            HerdMissionUI missionUI = card.GetComponent<HerdMissionUI>();
            missionCards.Remove(missionUI);
            Destroy(card.gameObject);
        }

        public void RemoveAllMissionCards() {
            for (int i = missionCards.Count - 1; i >= 0; i--) {
                Destroy(missionCards[i].gameObject);
                missionCards.RemoveAt(i);
            }
        }
    }
}
