using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.TimeSystem
{
    [Serializable]
    public class SeasonColors
    {
        public SeasonName season;
        public Color[] lightColours = new Color[4];
        public Color[] skyColours = new Color[4];
    }
}
