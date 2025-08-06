using System;
using System.Collections.Generic;
using UnityEngine;

public class Boids : MonoBehaviour
{
    private Rigidbody rb;
    public bool activeBoids;
    [HideInInspector] public Vector3 velocity;
    [SerializeField] private Boids[] boids;
    [SerializeField] private BoidData data;

    [Space(10)]
    [SerializeField] private bool showGizmos;

    private void Awake() => rb = GetComponent<Rigidbody>();

    public void ApplyForce() {
        if (activeBoids && boids.Length > 0) {
            velocity = rb.linearVelocity;
            boids = Neighbours();
            
            Vector3 totalForce = Cohesion() + Separation() + Alignment();
            rb.AddForce(totalForce);
        }
    }

    /// <summary>
    /// Gets all nearby boids within the boids radius (red)
    /// </summary>
    /// <returns>array of nearby boids</returns>
    private Boids[] Neighbours() {
        Collider[] cols = Physics.OverlapSphere(transform.position, data.radius);
        List<Boids> neighbours = new List<Boids>();
        
        foreach (Collider col in cols) {
            Boids b = col.GetComponent<Boids>();
            if (b && b != this) neighbours.Add(b);
        }
        return neighbours.ToArray();
    }

    /// <summary>
    /// Gets the middle most position of surrounding boids
    /// </summary>
    /// <returns>the strength of the force towards the center</returns>
    private Vector3 Cohesion() {
        Vector3 totalPos = Vector3.zero;
        foreach (Boids b in boids) {
            totalPos += b.transform.position;
        }
        Vector3 centerPos = totalPos/boids.Length;
        Vector3 dir = Vector3.Normalize(centerPos - transform.position);
        
        return dir * data.cohesion;
    }
    
    /// <summary>
    /// the point furthest away from all surrounding boids
    /// </summary>
    /// <returns>the direction the boid should travel to avoid nearby boids</returns>
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
            steeringForce = steeringForce.normalized - rb.linearVelocity;
        }
        
        return steeringForce * data.separation;
    }

    /// <summary>
    /// gets the average velocity of the surrounding boids moves in the average direction
    /// </summary>
    /// <returns>direction that is the average of surrounding boids</returns>
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
        if (showGizmos) {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, data.radius);
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, data.minSeparation);
        }
    }
}
