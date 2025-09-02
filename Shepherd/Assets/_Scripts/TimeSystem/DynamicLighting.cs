using System;
using _Scripts.TimeSystem;
using UnityEngine;
using UnityEngine.Serialization;

public class DynamicLighting : MonoBehaviour
{
    [SerializeField] private Light light;

    [SerializeField, Tooltip("Gap between night and sunset and sunrise")]
    private float transitionTime = 10f;

    [SerializeField, Tooltip("SunRise, Day, SunSet, Night")]
    private Color[] lightColors = new Color[4];

    [SerializeField] private Color[] skyColors = new Color[4];

    [SerializeField] private Gradient lightGradient;
    [SerializeField] private Gradient skyGradient;
    
    [SerializeField] private SeasonColors[] seasonColours;

    private void Awake() {
        SetGradient(lightGradient, lightColors);
        SetGradient(skyGradient, skyColors);
    }

    private void Update() {
        if (TimeManager.Instance != null) {
            float t = TimeManager.Instance.time.Progress;
            light.color = lightGradient.Evaluate(t);
            RenderSettings.skybox.SetColor("_Tint", skyGradient.Evaluate(t));
            LightAngle(t);
        }
    }

    private void LightAngle(float t) {
        float xAngle = 25 + (Mathf.Cos(t * 2 * Mathf.PI) + 1) / 2 * 25;
        float yAngle = Mathf.Lerp(0, 360, t) - 80;

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