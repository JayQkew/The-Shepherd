using System;
using UnityEngine;
using UnityEngine.Serialization;

public class DynamicLighting : MonoBehaviour
{
    private Light light;

    [SerializeField, Tooltip("Gap between night and sunset and sunrise")]
    private float twilightTime = 0.1f;

    [SerializeField, Tooltip("SunRise, Day, SunSet, Night")]
    private Color[] lightColors = new Color[4];

    [SerializeField] private Color[] skyColors = new Color[4];

    [SerializeField] private Gradient lightGradient;
    [SerializeField] private Gradient skyGradient;

    private void Awake() {
        light = GetComponent<Light>();

        SetGradient(lightGradient, lightColors);
        SetGradient(skyGradient, skyColors);
    }

    private void Update() {
        if (TimeManager.Instance != null) {
            float t = Mathf.Lerp(0, 1, TimeManager.Instance.time.Progress);
            light.color = lightGradient.Evaluate(t);
            RenderSettings.skybox.SetColor("_Tint", skyGradient.Evaluate(t));
            LightAngle(t);
        }
    }

    private void LightAngle(float t) {
        float xAngle = 25 + (Mathf.Cos(t * 2 * Mathf.PI) + 1) / 2 * 25;
        float yAngle = Mathf.Lerp(0, 360, t) - 80;

        transform.eulerAngles = new Vector3(xAngle, yAngle, transform.eulerAngles.z);
    }

    private void SetGradient(Gradient gradient, Color[] colors) {
        if (TimeManager.Instance == null) return;

        float totalSpanTime = TimeManager.Instance.time.maxTime + twilightTime;

        GradientColorKey[] colorKeys = new GradientColorKey[7];

        //Sunrise
        float curr = 0;
        curr += twilightTime / totalSpanTime;
        colorKeys[0] = new GradientColorKey(colors[0], curr);

        //Day
        curr += (TimeManager.Instance.sunRise.span - twilightTime) / totalSpanTime;
        colorKeys[1] = new GradientColorKey(colors[1], curr);

        //Day
        curr += TimeManager.Instance.day.span / totalSpanTime;
        colorKeys[2] = new GradientColorKey(colors[1], curr);

        //Sunset
        curr += TimeManager.Instance.sunSet.span / totalSpanTime;
        colorKeys[3] = new GradientColorKey(colors[2], curr);

        //Night
        curr += twilightTime / totalSpanTime;
        colorKeys[4] = new GradientColorKey(colors[3], curr);

        //Night
        curr += (TimeManager.Instance.night.span - twilightTime) / totalSpanTime;
        colorKeys[5] = new GradientColorKey(colors[3], curr);

        //Sunrise
        colorKeys[6] = new GradientColorKey(colors[0], 1.0f);

        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
        alphaKeys[0] = new GradientAlphaKey(1.0f, 0.0f);
        alphaKeys[1] = new GradientAlphaKey(1.0f, 1.0f);

        gradient.SetKeys(colorKeys, alphaKeys);
    }

    [ContextMenu("Update Gradient")]
    public void UpdateGradient() {
        light = GetComponent<Light>();
        SetGradient(lightGradient, lightColors);
        SetGradient(skyGradient, skyColors);
    }

    private void OnValidate() {
        if (Application.isPlaying && TimeManager.Instance != null) {
            light = GetComponent<Light>();
            SetGradient(lightGradient, lightColors);
            SetGradient(skyGradient, skyColors);
        }
    }
}