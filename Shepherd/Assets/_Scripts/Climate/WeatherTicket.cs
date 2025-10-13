using System;
using Ambience;
using UnityEngine;
using Utilities;
using Random = System.Random;

namespace Climate
{
    [CreateAssetMenu(fileName = "NewWeatherTicket", menuName = "Climate/WeatherTicket")]
    public class WeatherTicket : ScriptableObject
    {
        public WeatherType weatherType;
        public MinMax tempDelta;
        public AmbienceSource ambienceSource;
        
        public float TempDelta(float intensity) {
            return tempDelta.Lerp(intensity);
        }
    }
}
