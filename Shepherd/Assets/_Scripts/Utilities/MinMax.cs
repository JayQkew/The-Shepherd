using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utilities
{
    [Serializable]
    public struct MinMax
    {
        [SerializeField] private float _min;
        [SerializeField] private float _max;

        public float min
        {
            get => _min;
            set
            {
                _min = value;
                if (_min > _max) _max = _min;
            }
        }

        public float max
        {
            get => _max;
            set
            {
                _max = value;
                if (_max < _min) _min = _max;
            }
        }

        public MinMax(float min, float max) {
            _min = Mathf.Min(min, max);
            _max = Mathf.Max(min, max);
        }

        public float RandomValue() {
            return Random.Range(min, max);
        }

        public float Lerp(float t) {
            return Mathf.Lerp(min, max, t);
        }

        public float InverseLerp(float value) {
            return Mathf.InverseLerp(min, max, value);
        }

        public float Clamp(float value) {
            return Mathf.Clamp(value, min, max);
        }

        public bool Contains(float value) {
            return value >= min && value <= max;
        }

        public float Range => max - min;
        public float Mid => (min + max) * 0.5f;

        public override string ToString() {
            return $"MinMax({min:F2}, {max:F2})";
        }
    }

    [Serializable]
    public struct MinMaxInt
    {
        [SerializeField] private int _min;
        [SerializeField] private int _max;

        public int min
        {
            get => _min;
            set
            {
                _min = value;
                if (_min > _max) _max = _min;
            }
        }

        public int max
        {
            get => _max;
            set
            {
                _max = value;
                if (_max < _min) _min = _max;
            }
        }

        public MinMaxInt(int min, int max) {
            _min = Mathf.Min(min, max);
            _max = Mathf.Max(min, max);
        }

        public int RandomValue() {
            return Random.Range(min, max + 1);
        }

        public int RandomValueExclusive() {
            return Random.Range(min, max);
        }

        public int Clamp(int value) {
            return Mathf.Clamp(value, min, max);
        }

        public bool Contains(int value) {
            return value >= min && value <= max;
        }

        public int Range => max - min;
        public float Mid => (min + max) * 0.5f;

        public override string ToString() {
            return $"MinMaxInt({min}, {max})";
        }
    }
}