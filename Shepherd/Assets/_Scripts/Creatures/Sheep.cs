using System;
using UnityEngine;

public class Sheep : MonoBehaviour, IBarkable
{
    private Rigidbody _rb;
    [SerializeField] private float barkForce;

    private void Awake() {
        _rb = GetComponent<Rigidbody>();
    }

    public void BarkedAt(Vector3 sourcePosition) {
        Vector3 dir = (transform.position - sourcePosition).normalized;
        _rb.AddForce(dir * 10, ForceMode.Impulse);
        Debug.Log("Barked At");
    }
}
