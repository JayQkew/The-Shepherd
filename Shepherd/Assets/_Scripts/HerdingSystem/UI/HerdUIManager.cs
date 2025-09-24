using System.Collections.Generic;
using UnityEngine;

namespace HerdingSystem
{
    public class HerdUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject missionCard;
        [SerializeField] private Transform missionCardContainer;
        public List<HerdMissionUI> missionCards = new List<HerdMissionUI>();

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
