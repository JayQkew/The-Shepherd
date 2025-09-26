using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace TimeSystem
{
    public class DynamicLighting : MonoBehaviour
    {
        private static readonly int Tint = Shader.PropertyToID("_Tint");
        [SerializeField] private Light light;

        [SerializeField, Tooltip("Gap between night and sunset and sunrise")]
        private float transitionTime = 10f;

        [SerializeField, Tooltip("SunRise, Day, SunSet, Night")]
        private Color[] lightColors = new Color[4];
        [SerializeField] private Color[] skyColors = new Color[4];

        [SerializeField] private Gradient lightGradient;
        [SerializeField] private Gradient skyGradient;
        [SerializeField] private AnimationCurve intensityCurve;
        [Space(25)]
        [SerializeField] private Volume volume;
        [SerializeField] private AnimationCurve hueShiftCurve;
        private ColorAdjustments colorAdjustments;
    
        private void Awake() {
            UpdateGradient();

            if (volume != null) {
                volume.profile.TryGet(out colorAdjustments);
            }
        }

        private void Update() {
            if (TimeManager.Instance != null) {
                float t = TimeManager.Instance.time.Progress;
                float yearT = TimeManager.Instance.yearTimer.Progress;
                light.color = lightGradient.Evaluate(t);
                RenderSettings.skybox.SetColor(Tint, skyGradient.Evaluate(t));
                LightAngle(t);
                light.intensity = intensityCurve.Evaluate(t);
                
                ClampedFloatParameter hueShift = new ClampedFloatParameter(
                    hueShiftCurve.Evaluate(yearT),
                    colorAdjustments.hueShift.min,
                    colorAdjustments.hueShift.max);
                
                colorAdjustments.hueShift.value = hueShift.value;
            }
        }

        private void LightAngle(float t) {
            float xAngle = 15 + Mathf.Abs(Mathf.Sin(t * 2 * Mathf.PI)) * 15;
            float yAngle = Mathf.Lerp(-90f, 90f, Mathf.Repeat(t * 2, 1f));

            light.transform.eulerAngles = new Vector3(xAngle, yAngle, transform.eulerAngles.z);
        }

        private void SetGradient(Gradient gradient, Color[] colors) {
            if (TimeManager.Instance == null) return;

            float totalSpanTime = TimeManager.Instance.time.maxTime + transitionTime * 2;

            GradientColorKey[] colorKeys = new GradientColorKey[8];

            //Sunrise - end
            float curr = 0;
            curr += (TimeManager.Instance.dayPhases[0].timer.maxTime - transitionTime / 2) / totalSpanTime;
            colorKeys[0] = new GradientColorKey(colors[0], curr);

            //Day - start
            curr += transitionTime / totalSpanTime;
            colorKeys[1] = new GradientColorKey(colors[1], curr);

            //Day - end
            curr += (TimeManager.Instance.dayPhases[1].timer.maxTime - transitionTime / 2) / totalSpanTime;
            colorKeys[2] = new GradientColorKey(colors[1], curr);

            //Sunset - start
            curr += transitionTime / totalSpanTime;
            colorKeys[3] = new GradientColorKey(colors[2], curr);

            //Sunset - end
            curr += (TimeManager.Instance.dayPhases[2].timer.maxTime - transitionTime / 2) / totalSpanTime;
            colorKeys[4] = new GradientColorKey(colors[2], curr);

            //Night - start
            curr += transitionTime / totalSpanTime;
            colorKeys[5] = new GradientColorKey(colors[3], curr);
        
            //Night - end
            curr += (TimeManager.Instance.dayPhases[3].timer.maxTime - transitionTime / 2) / totalSpanTime;
            colorKeys[6] = new GradientColorKey(colors[3], curr);

            //Sunrise - start
            colorKeys[7] = new GradientColorKey(colors[0], 1.0f);

            GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
            alphaKeys[0] = new GradientAlphaKey(1.0f, 0.0f);
            alphaKeys[1] = new GradientAlphaKey(1.0f, 1.0f);

            gradient.SetKeys(colorKeys, alphaKeys);
        }

        private void UpdateGradient() {
            SetGradient(lightGradient, lightColors);
            SetGradient(skyGradient, skyColors);
        }

        private void OnValidate() {
            if (TimeManager.Instance != null) {
                UpdateGradient();
            }
        }
    }
}