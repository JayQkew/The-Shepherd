using System;
using UnityEngine;
using Utilities;

namespace Climate
{
    [Serializable]
    public class WeatherTicket
    {
        public WeatherType weatherType;
        public MinMax tempDelta;
        public MinMax intensity;
    }
}
