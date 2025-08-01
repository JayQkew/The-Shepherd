using UnityEngine;
[CreateAssetMenu(fileName = "BoidData", menuName = "BoidData")]
public class BoidData : ScriptableObject
{
    [Tooltip("The radius of effect (Red)")] 
    public float radius;
    [Tooltip("The force keeping the boids together")] 
    public float cohesion;
    [Tooltip("The force keeping boids apart")] 
    public float separation;
    [Tooltip("how much the boid wants to face same direction as nearby boids. This is based on velocity.")] 
    public float alignment;
    [Tooltip("The minimum distance to keep away from the nearby boids (Yellow)")]
    public float minSeparation;
}
