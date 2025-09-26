using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Creatures
{
    [Serializable]
    public class Poop
    {
        [SerializeField] private GameObject poopPref;
        [SerializeField] private Transform poopSpawn;
        [SerializeField] private float poopForce;

        public void ShootPoop() {
            Rigidbody poopRb = Object.Instantiate(poopPref, poopSpawn.position, Quaternion.identity)
                .GetComponent<Rigidbody>();
            poopRb.AddForce(poopForce * poopSpawn.up, ForceMode.Impulse);
        }
    }
}