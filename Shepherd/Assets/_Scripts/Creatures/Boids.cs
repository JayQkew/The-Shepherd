using System;
using System.Collections.Generic;
using UnityEngine;

public class Boids : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private float radius;
    [SerializeField] private Boids[] boids;

    private void Awake() => _rb = GetComponent<Rigidbody>();

    private void FixedUpdate() {
        boids = Neighbours();
    }

    private Boids[] Neighbours() {
        Collider[] cols = Physics.OverlapSphere(transform.position, radius);
        List<Boids> neighbours = new List<Boids>();
        
        foreach (Collider col in cols) {
            Boids b = col.GetComponent<Boids>();
            if (b && b != this) neighbours.Add(b);
        }
        return neighbours.ToArray();
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
