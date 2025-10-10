using System;
using UnityEngine;

namespace Ambience
{
    [Serializable]
    public class HueShift : ProfileData
    {
        [Tooltip("< 0 is cooler & > 0 is warmer")]
        public float value = 0;

        protected override void ProcessInternal(ProfileData tempData) {
            HueShift tempProfileData = tempData as HueShift;

            tempProfileData!.value += value;
        }
    }
}
