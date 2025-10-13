using System;
using UnityEngine;

namespace Ambience
{
    [Serializable]
    public class Light : ProfileData
    {
        public static float TotalIntensity;
        public static int Count;
        [Tooltip("Tint applied on top of gradient")]
        public Color color = Color.white;
        [Tooltip("Multiplier for the intensity of the light")]
        public float intensity = 1f;

        public Light() {
            TotalIntensity = 1;
            Count = 1;
        }

        public Light(Color color, float intensity) {
            this.color = color;
            this.intensity = intensity;
        }

        public Light Clone() {
            Light newLight = new Light (color, intensity);

            return newLight;
        }

        protected override void ProcessInternal(ProfileData tempData) {
            Light tempProfileData = tempData as Light;
            if (Count == 1) {
                tempProfileData.color = color;
            } else {
                // Otherwise, lerp with proper weighting
                float weight = 1f / Count;
                tempProfileData.color = Color.Lerp(tempProfileData.color, color, weight);
            }
            tempProfileData.color.a = 1;
            TotalIntensity += intensity;
            Count++;
        }
    }
}