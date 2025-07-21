using System;
using UnityEngine;

public class DynamicLighting : MonoBehaviour
{
    private Light light;

    [SerializeField, Tooltip("Gap between night and sunset and sunrise")]
    private float twilightTime = 0.1f;

    [SerializeField, Tooltip("SunRise, Day, SunSet, Night")]
    private Color[] colors = new Color[4];

    [SerializeField] private Gradient gradient;

    private void Awake() {
        light = GetComponent<Light>();

        SetLightGradient();
    }

    private void Update() {
        if (TimeManager.Instance != null) {
            float t = Mathf.Lerp(0, 1, TimeManager.Instance.currTime / TimeManager.Instance.maxTime);
            light.color = gradient.Evaluate(t);
        }
    }

    private void SetLightGradient() {
        if (TimeManager.Instance == null) return;
        gradient = new Gradient();

        float totalSpanTime = TimeManager.Instance.maxTime + twilightTime;

        GradientColorKey[] colorKeys = new GradientColorKey[7];

        //Sunrise
        float curr = 0;
        colorKeys[0] = new GradientColorKey(colors[0], curr);
        Debug.Log(curr);

        //Day
        curr += TimeManager.Instance.sunRise.span / totalSpanTime;
        colorKeys[1] = new GradientColorKey(colors[1], curr);
        Debug.Log(curr);

        //Day
        curr += TimeManager.Instance.day.span / totalSpanTime;
        colorKeys[2] = new GradientColorKey(colors[1], curr);
        Debug.Log(curr);

        //Sunset
        curr += TimeManager.Instance.sunSet.span / totalSpanTime;
        colorKeys[3] = new GradientColorKey(colors[2], curr);
        Debug.Log(curr);

        //Night
        curr += twilightTime / totalSpanTime;
        colorKeys[4] = new GradientColorKey(colors[3], curr);
        Debug.Log(curr);

        //Night
        curr += (TimeManager.Instance.night.span - twilightTime) / totalSpanTime;
        colorKeys[5] = new GradientColorKey(colors[3], curr);
        Debug.Log(curr);

        //Sunrise
        colorKeys[6] = new GradientColorKey(colors[0], 1.0f);
        Debug.Log(curr);

        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
        alphaKeys[0] = new GradientAlphaKey(1.0f, 0.0f);
        alphaKeys[1] = new GradientAlphaKey(1.0f, 1.0f);

        gradient.SetKeys(colorKeys, alphaKeys);
    }

    [ContextMenu("Update Gradient")]
    public void UpdateGradient() {
        light = GetComponent<Light>();
        SetLightGradient();
    }

    private void OnValidate() {
        if (Application.isPlaying && TimeManager.Instance != null) {
            light = GetComponent<Light>();
            SetLightGradient();
        }
    }
}