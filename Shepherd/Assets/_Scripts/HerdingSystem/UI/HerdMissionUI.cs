using TMPro;
using UnityEngine;
using Utilities;

namespace HerdingSystem.UI
{
    public class HerdMissionUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI destinationTxt;
        [SerializeField] private TextMeshProUGUI animalTxt;
        [SerializeField] private TextMeshProUGUI numberTxt;

        public void Init(HerdMission herdMission) {
            string destination = herdMission.destination.StringValue();
            string animal = herdMission.animal.StringValue();
            destinationTxt.text = "Destination: " + destination;
            animalTxt.text = "Animal: " + animal;
            numberTxt.text = herdMission.curr + " / " + herdMission.target;
        }
    }
}
