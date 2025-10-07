using System;
using TimeSystem;
using UnityEngine;

namespace Ambience
{
    [Serializable]
    public class AmbientLighting
    {
        [SerializeField] private LightingData data;
        [SerializeField] private Light light;
        
        private static readonly int Tint = Shader.PropertyToID("_Tint");
        
        public void UpdateLighting() {
            if (TimeManager.Instance != null) {
                float t = TimeManager.Instance.time.Progress;
                light.color = data.lightGradient.Evaluate(t);
                RenderSettings.skybox.SetColor(Tint, data.skyGradient.Evaluate(t));
                LightAngle(t);
                light.intensity = data.intensityCurve.Evaluate(t);
            }
        }

        private void LightAngle(float t) {
            float xAngle = 15 + Mathf.Abs(Mathf.Sin(t * 2 * Mathf.PI)) * 15;
            float yAngle = Mathf.Lerp(-90f, 90f, Mathf.Repeat(t * 2, 1f));

            light.transform.eulerAngles = new Vector3(xAngle, yAngle, light.transform.eulerAngles.z);
        }
    }
}