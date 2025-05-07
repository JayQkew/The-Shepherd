using System;
using System.Collections.Generic;
using UnityEngine;

public class Boids : MonoBehaviour
{
    private Rigidbody _rb;
    [HideInInspector] public Vector3 velocity;
    [SerializeField] private Boids[] boids;
    [SerializeField] private BoidData data;

    private void Awake() => _rb = GetComponent<Rigidbody>();

    public void ApplyForce() {
        velocity = _rb.linearVelocity;
        boids = Neighbours();
        
        if(boids.Length <= 0) return;
        
        Vector3 totalForce = Cohesion() + Separation() + Alignment();
        _rb.AddForce(totalForce);
    }

    private Boids[] Neighbours() {
        Collider[] cols = Physics.OverlapSphere(transform.position, data.radius);
        List<Boids> neighbours = new List<Boids>();
        
        foreach (Collider col in cols) {
            Boids b = col.GetComponent<Boids>();
            if (b && b != this) neighbours.Add(b);
        }
        return neighbours.ToArray();
    }

    private Vector3 Cohesion() {
        Vector3 totalPos = Vector3.zero;
        foreach (Boids b in boids) {
            totalPos += b.transform.position;
        }
        Vector3 centerPos = totalPos/boids.Length;
        Vector3 dir = Vector3.Normalize(centerPos - transform.position);
        
        return dir * data.cohesion;
    }
    
    private Vector3 Separation() {
        Vector3 steeringForce = Vector3.zero;
        int count = 0;
        
        foreach (Boids b in boids) {
            Vector3 offset = transform.position - b.transform.position;
            float distance = offset.magnitude;
            
            if (distance < data.minSeparation && distance > 0) {
                Vector3 repulsionDir = offset.normalized;
                float repulsionStrength = 1.0f / Mathf.Max(0.1f, distance);
                steeringForce += repulsionDir * repulsionStrength;
                count++;
            }
        }

        if (count > 0) {
            steeringForce /= count;
            steeringForce = steeringForce.normalized - _rb.linearVelocity;
        }
        
        return steeringForce * data.separation;
    }

    private Vector3 Alignment() {
        Vector3 aveVelocity = Vector3.zero;
        foreach (Boids b in boids){
            aveVelocity += b.velocity;
        }
        aveVelocity /= boids.Length;
        Vector3 targetVelocity = aveVelocity - velocity;
        
        return targetVelocity * data.alignment;
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, data.radius);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, data.minSeparation);
    }
}
