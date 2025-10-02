using UnityEngine;

namespace Boids
{
    public class BoidsManager : MonoBehaviour
    {
        [SerializeField] private Transform boidsParent;
        [SerializeField] private Boids[] boids;

        private void Start() {
            // all boids should be a child of boidsParent
            boids = boidsParent.GetComponentsInChildren<global::Boids.Boids>();
        }

        private void FixedUpdate() {
            foreach (var b in boids) {
                b.ApplyForce();
            }
        }
    }
}
