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
        [SerializeField] private HueShift hueShiftProfileData;
        
        private ColorAdjustments colorAdjustments;
        public override void Init() {
            if (volume != null) {
                volume.profile.TryGet(out colorAdjustments);
            }
        }
        
        public override void UpdateModule() {
            if (TimeManager.Instance != null) {
                float year = TimeManager.Instance.yearTimer.Progress;

                ClampedFloatParameter hueShift = new ClampedFloatParameter(
                    data.hueShiftCurve.Evaluate(year) + hueShiftProfileData.value,
                    colorAdjustments.hueShift.min,
                    colorAdjustments.hueShift.max);

                colorAdjustments.hueShift.value = hueShift.value;
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
                        if (profileData is HueShift hueShiftData) hueShiftData.Process(tempHueShift);
                    }
                }
            }
            
            hueShiftProfileData = tempHueShift;
            
            base.TotalProfiles();
        }

        public override void ApplyProfiles() { }
    }
}