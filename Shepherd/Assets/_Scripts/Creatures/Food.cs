using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Food : MonoBehaviour
{
    [Header("Eating")]
    [SerializeField] private float food;
    [SerializeField] private MinMax eat;

    [Header("Poop")]
    [SerializeField] private GameObject poopPref;
    [SerializeField] private Transform poopSpawn;
    [SerializeField] private float poopForce;
    [SerializeField] private float poopDelay;
    [SerializeField] private float poopThreshold;
    
    public void Eat() {
        food += eat.RandomValue();
        if (food >= 1) Poop();
    }
    
    private void Poop() {
        food = 0;
        Rigidbody poopRb = Instantiate(poopPref, poopSpawn.position, Quaternion.identity, transform).GetComponent<Rigidbody>();
        poopRb.AddForce(poopForce * poopSpawn.up, ForceMode.Impulse);
    }
}
