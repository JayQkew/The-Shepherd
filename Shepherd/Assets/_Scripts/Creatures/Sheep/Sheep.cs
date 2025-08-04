using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Sheep : MonoBehaviour, IBarkable
{
    private Rigidbody rb;
    [SerializeField] private float barkForce;
    
    [SerializeField] private float weight;
    [SerializeField] private float wool;
    [SerializeField] private float food;
    [SerializeField] private MinMax eat;

    [Header("Poop")]
    [SerializeField] private GameObject poopPref;
    [SerializeField] private Transform poopSpawn;
    [SerializeField] private float poopForce;
    [SerializeField] private bool canPoop;
    [SerializeField] private float poopDelay;
    [SerializeField] private float poopThreshold;
    
    [SerializeField] private float currWeight;
    [SerializeField] private float maxWeight;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        canPoop = true;
    }

    public void BarkedAt(Vector3 sourcePosition) {
        Vector3 dir = (transform.position - sourcePosition).normalized;
        rb.AddForce(dir * 10, ForceMode.Impulse);
        Debug.Log("Barked At");
    }

    public void WoolToWeight() {
        weight = 1 - wool;
        currWeight = weight * maxWeight;
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
        Debug.Log(name + " just POOOOOOPED");
        Rigidbody poopRb = Instantiate(poopPref, poopSpawn.position, Quaternion.identity, transform).GetComponent<Rigidbody>();
        poopRb.AddForce(poopForce * poopSpawn.up, ForceMode.Impulse);
        //play poop effects
    }

    private IEnumerator PoopDelay() {
        canPoop = false;
        float rand = Random.Range(0f, 1f);
        yield return new WaitForSeconds(rand * poopDelay);
        Poop();
        canPoop = true;
    }
}
