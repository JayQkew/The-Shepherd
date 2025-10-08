using System;
using UnityEngine;

namespace Climate
{
    [Serializable]
    public class WeatherManager
    {
        public WeatherType currWeatherType;
    }

    [Flags]
    public enum WeatherType
    {
        None = 0,
        Rain = 1 << 0,
        Cloudy = 1 << 1,
        Sunny = 1 << 2,
        Windy = 1 << 3
    }
}
