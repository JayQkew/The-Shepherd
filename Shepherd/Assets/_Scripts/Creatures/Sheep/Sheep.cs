using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Sheep : MonoBehaviour, IBarkable
{
    private Rigidbody rb;
    private SheepStateManager sheepStateManager;
    [SerializeField] private float barkForce;
    
    [Header("Wool")]
    [SerializeField] private float wool;
    [SerializeField] private float woolCurrTime;
    [SerializeField] private float woolGrowTime;
    [SerializeField] private float currWeight;
    [SerializeField] private MinMax weight;
    
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
        rb = GetComponent<Rigidbody>();
        sheepStateManager = GetComponent<SheepStateManager>();
        canPoop = true;
    }

    public void BarkedAt(Vector3 sourcePosition) {
        Vector3 dir = (transform.position - sourcePosition).normalized;
        rb.AddForce(dir * 10, ForceMode.Impulse);
        sheepStateManager.SwitchState(sheepStateManager.sheepRun);
        Debug.Log("Barked At");
    }

    private void Update() {
        GrowWool();
    }
    
    private void GrowWool() {
        woolCurrTime += Time.deltaTime;
        woolCurrTime = Mathf.Clamp(woolCurrTime, 0, woolGrowTime);
        wool = woolCurrTime / woolGrowTime;
        rb.mass = weight.Lerp(wool);
    }
}
