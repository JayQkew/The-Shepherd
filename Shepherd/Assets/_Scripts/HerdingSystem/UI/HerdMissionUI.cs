using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utilities;

namespace HerdingSystem
{
    public class HerdMissionUI : MonoBehaviour
    {
        private HerdMission mission;
        [SerializeField] private TextMeshProUGUI descriptionTxt;
        [SerializeField] private TextMeshProUGUI numberTxt;
        [SerializeField] private Slider slider;

        public void Init(HerdMission herdMission) {
            mission = herdMission;
            string destination = herdMission.destination.StringValue();
            string animal = herdMission.animal.StringValue();
            descriptionTxt.text = $"Herd {animal} to {destination}";
            
            numberTxt.text = herdMission.curr + " / " + herdMission.target;
            slider.minValue = herdMission.curr;
            slider.maxValue = herdMission.target;
        }

        public void UpdateNumbers() {
            numberTxt.text = mission.curr + " / " + mission.target;
            slider.value = mission.curr;
        }
    }
}
