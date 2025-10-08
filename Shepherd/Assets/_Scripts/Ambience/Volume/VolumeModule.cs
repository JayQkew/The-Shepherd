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
        [SerializeField] private VolumeData data;
        [SerializeField] private UnityEngine.Rendering.Volume volume;
        private ColorAdjustments colorAdjustments;

        public void Init() {
            if (volume != null) {
                volume.profile.TryGet(out colorAdjustments);
            }
        }

        public void UpdateVolume() {
            float year = TimeManager.Instance.yearTimer.Progress;
            
            ClampedFloatParameter hueShift = new ClampedFloatParameter(
                data.hueShiftCurve.Evaluate(year),
                colorAdjustments.hueShift.min,
                colorAdjustments.hueShift.max);
                
            colorAdjustments.hueShift.value = hueShift.value;
        }
    }
}