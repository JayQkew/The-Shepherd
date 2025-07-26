using System;
using UnityEngine;

public class Sheep : MonoBehaviour, IBarkable
{
    private Rigidbody rb;
    [SerializeField] private float barkForce;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    public void BarkedAt(Vector3 sourcePosition) {
        Vector3 dir = (transform.position - sourcePosition).normalized;
        rb.AddForce(dir * 10, ForceMode.Impulse);
        Debug.Log("Barked At");
    }
}
