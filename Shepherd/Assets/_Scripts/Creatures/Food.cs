using System;
using Creatures;
using UnityEngine;
using Utilities;
using Object = UnityEngine.Object;

namespace _Scripts.Creatures
{
    [Serializable]
    public class Food
    {
        private Transform transform;
        [SerializeField] private float food;
        [SerializeField] private MinMax eat;
        [Space(10)]
        [SerializeField] private Poop poop;

        public void Init(Transform trans) {
            transform = trans;
        }
    
        public void Eat() {
            food += eat.RandomValue();
            if (food >= 1) {
                food = 0;
                poop.ShootPoop();
            }
        }
    }
}