using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Sheep : MonoBehaviour, IBarkable
{
    private Rigidbody rb;
    [SerializeField] private float barkForce;
    
    [SerializeField] private float weight;
    [SerializeField] private float wool;
    [SerializeField] private float food;
    [SerializeField] private MinMax eat;
    [SerializeField] private bool canPoop;
    
    [SerializeField] private float currWeight;
    [SerializeField] private float maxWeight;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
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
        canPoop = food >= 0.85f;
    }

    private void Poop() {
        food -= Random.Range(0.3f, food);
        Debug.Log(name + " just POOOOOOPED");
        //play poop effects
    }
}
