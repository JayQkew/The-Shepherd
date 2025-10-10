using System;
using TimeSystem;
using UnityEngine;

namespace Ambience
{
    [Serializable]
    public class LightingModule : Module
    {
        public override AmbienceType AmbienceType => AmbienceType.Lighting;

        [SerializeField] private LightingData data;
        [SerializeField] private UnityEngine.Light light;
        [SerializeField] private Gradient lightGradient;
        [SerializeField] private Gradient skyboxGradient;

        [Space(15)]
        [SerializeField, Range(0, 1)] private float gradientTint;

        [SerializeField] private Gradient currLightGradient;
        [SerializeField] private Gradient currSkyboxGradient;

        [Space(15)]
        [SerializeField] private Light lightProfileData;
        [SerializeField] private Skybox skyboxProfileData;
        
        private static readonly int Tint = Shader.PropertyToID("_Tint");

        public override void TotalProfiles() {
            Light tempLight = new Light();
            Skybox tempSkybox = new Skybox();

            foreach (Profile profile in Profiles) {
                LightingProfile lightingProfile = profile as LightingProfile;

                if (lightingProfile == null) {
                    Debug.LogWarning("LightingProfile not in LightingProfile!");
                    continue;
                }

                ProfileData[] profileDatas = lightingProfile.GetProfileDatas();

                foreach (ProfileData profileData in profileDatas) {
                    if (profileData.Use) {
                        if (profileData is Light lightData) lightData.Process(tempLight);
                        else if (profileData is Skybox skyboxData) skyboxData.Process(tempSkybox);
                    }
                }
            }

            lightProfileData = tempLight;
            skyboxProfileData = tempSkybox;

            base.TotalProfiles();
        }

        public override void ApplyProfiles() {
            currLightGradient = TintGradient(lightGradient, lightProfileData.color);
            currSkyboxGradient = TintGradient(skyboxGradient, skyboxProfileData.color);
        }

        public Gradient TintGradient(Gradient original, Color tint) {
            Gradient gradient = new Gradient();

            GradientColorKey[] tintedColorKeys = original.colorKeys;

            for (int i = 0; i < tintedColorKeys.Length; i++) {
                tintedColorKeys[i].color = Color.Lerp(tintedColorKeys[i].color, tint, gradientTint);
            }

            gradient.SetKeys(
                tintedColorKeys,
                original.alphaKeys
            );
            return gradient;
        }

        public void UpdateLighting() {
            if (TimeManager.Instance != null) {
                float t = TimeManager.Instance.dayTime.Progress;
                light.color = currLightGradient.Evaluate(t);
                RenderSettings.skybox.SetColor(Tint, currSkyboxGradient.Evaluate(t));
                LightAngle(t);
                light.intensity = data.intensityCurve.Evaluate(t) * Light.CalculatedIntensity;
            }
        }

        private void LightAngle(float t) {
            float xAngle = 15 + Mathf.Abs(Mathf.Sin(t * 2 * Mathf.PI)) * 15;
            float yAngle = Mathf.Lerp(-90f, 90f, Mathf.Repeat(t * 2, 1f));

            light.transform.eulerAngles = new Vector3(xAngle, yAngle, light.transform.eulerAngles.z);
        }

        private void SetGradient(Gradient gradient, Color[] colors) {
            if (TimeManager.Instance == null) return;

            float totalSpanTime = TimeManager.Instance.dayTime.maxTime + data.transitionTime * 2;

            GradientColorKey[] colorKeys = new GradientColorKey[8];

            //Sunrise - end
            float curr = 0;
            curr += (TimeManager.Instance.dayPhases[0].timer.maxTime - data.transitionTime / 2) / totalSpanTime;
            colorKeys[0] = new GradientColorKey(colors[0], curr);

            //Day - start
            curr += data.transitionTime / totalSpanTime;
            colorKeys[1] = new GradientColorKey(colors[1], curr);

            //Day - end
            curr += (TimeManager.Instance.dayPhases[1].timer.maxTime - data.transitionTime / 2) / totalSpanTime;
            colorKeys[2] = new GradientColorKey(colors[1], curr);

            //Sunset - start
            curr += data.transitionTime / totalSpanTime;
            colorKeys[3] = new GradientColorKey(colors[2], curr);

            //Sunset - end
            curr += (TimeManager.Instance.dayPhases[2].timer.maxTime - data.transitionTime / 2) / totalSpanTime;
            colorKeys[4] = new GradientColorKey(colors[2], curr);

            //Night - start
            curr += data.transitionTime / totalSpanTime;
            colorKeys[5] = new GradientColorKey(colors[3], curr);

            //Night - end
            curr += (TimeManager.Instance.dayPhases[3].timer.maxTime - data.transitionTime / 2) / totalSpanTime;
            colorKeys[6] = new GradientColorKey(colors[3], curr);

            //Sunrise - start
            colorKeys[7] = new GradientColorKey(colors[0], 1.0f);

            GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
            alphaKeys[0] = new GradientAlphaKey(1.0f, 0.0f);
            alphaKeys[1] = new GradientAlphaKey(1.0f, 1.0f);

            gradient.SetKeys(colorKeys, alphaKeys);
        }

        public void OnValidate() {
            if (TimeManager.Instance != null) {
                SetGradient(lightGradient, data.lightColors);
                SetGradient(skyboxGradient, data.skyColors);
            }
        }
    }
}