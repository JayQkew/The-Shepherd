using System;
using UnityEngine;

public class Lighting : MonoBehaviour
{
    private Light light;
    [SerializeField] private Gradient gradient;

    private void Awake() {
        light = GetComponent<Light>();
    }

    private void Update() {
        light.color = gradient.Evaluate(Mathf.Lerp(0, 1, TimeManager.Instance.currTime/TimeManager.Instance.maxTime));
    }
}
