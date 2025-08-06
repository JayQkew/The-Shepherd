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
    [SerializeField] private bool canPoop;
    [SerializeField] private float poopDelay;
    [SerializeField] private float poopThreshold;

    private void Awake() {
        canPoop = true;
    }

    public void Eat() {
        food += eat.RandomValue();
        canPoop = food >= poopThreshold;
        
        if (Mathf.Approximately(food, 1)) Poop();
        else if (food >= poopThreshold && canPoop) {
            float rand = Random.Range(0f, 1f);
            if (rand <= food) StartCoroutine(PoopDelay());
        }
    }
    
    private void Poop() {
        food -= Random.Range(0.3f, food);
        Rigidbody poopRb = Instantiate(poopPref, poopSpawn.position, Quaternion.identity, transform).GetComponent<Rigidbody>();
        poopRb.AddForce(poopForce * poopSpawn.up, ForceMode.Impulse);
    }

    private IEnumerator PoopDelay() {
        canPoop = false;
        float rand = Random.Range(0f, 1f);
        yield return new WaitForSeconds(rand * poopDelay);
        Poop();
        canPoop = true;
    }
}
