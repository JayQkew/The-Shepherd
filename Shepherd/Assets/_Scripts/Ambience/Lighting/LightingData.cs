using System;
using TimeSystem;
using UnityEngine;

namespace Ambience
{
    [CreateAssetMenu(fileName = "NewLightingData", menuName = "Ambience/Lighting")]
    public class LightingData : ScriptableObject
    {
        [SerializeField, Tooltip("Gap between night and sunset and sunrise")]
        public float transitionTime;
        [Header("Colours")]
        [Tooltip("SunRise, Day, SunSet, Night")] public Color[] lightColors;
        [Space(10)]
        [Tooltip("SunRise, Day, SunSet, Night")] public Color[] skyColors;
        public AnimationCurve intensityCurve;
    }
}
