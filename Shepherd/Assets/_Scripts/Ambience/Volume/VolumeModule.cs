using System;
using TimeSystem;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Ambience
{
    [Serializable]
    public class VolumeModule : Module
    {
        public override AmbienceType AmbienceType => AmbienceType.Volume;

        [SerializeField] private VolumeData data;
        [SerializeField] private Volume volume;

        [Space(15)]
        [SerializeField] private float currHueShift;

        [Space(15)]
        [SerializeField] private HueShift hueShiftProfileData;
        
        private ColorAdjustments colorAdjustments;
        public void Init() {
            if (volume != null) {
                volume.profile.TryGet(out colorAdjustments);
            }
        }
        
        public override void TotalProfiles() {
            HueShift tempHueShift = new HueShift();

            foreach (Profile profile in Profiles) {
                VolumeProfile volumeProfile = profile as VolumeProfile;

                if (volumeProfile == null) {
                    Debug.LogWarning("VolumeProfile not a VolumeProfile");
                    continue;
                }

                ProfileData[] profileDatas = volumeProfile.GetProfileDatas();

                foreach (ProfileData profileData in profileDatas) {
                    if (profileData.Use) {
                        if (profileData is HueShift hueShiftData) ProcessHueShift(hueShiftData, tempHueShift);
                    }
                }
            }
            
            hueShiftProfileData = tempHueShift;
            
            base.TotalProfiles();
        }

        public override void ApplyProfiles() {
            currHueShift = hueShiftProfileData.value;
        }

        private void ProcessHueShift(HueShift hueShiftData, HueShift tempProfileData) {
            tempProfileData.value += hueShiftData.value;
        }

        public void UpdateVolume() {
            if (TimeManager.Instance != null) {
                float year = TimeManager.Instance.yearTimer.Progress;

                ClampedFloatParameter hueShift = new ClampedFloatParameter(
                    data.hueShiftCurve.Evaluate(year) + currHueShift,
                    colorAdjustments.hueShift.min,
                    colorAdjustments.hueShift.max);

                colorAdjustments.hueShift.value = hueShift.value;
            }
        }
    }
}