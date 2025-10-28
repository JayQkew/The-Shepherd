using System.Collections.Generic;
using UnityEngine;

namespace Environment
{
    public class TreeLeavesParticles : MonoBehaviour
    {
        private ParticleSystem ps;
        private List<ParticleCollisionEvent> collisionEvents;
        private ParticleSystem.Particle[] particles;

        // Keep track of which particle indices have landed
        private HashSet<int> stuckParticles = new HashSet<int>();

        void Awake()
        {
            ps = GetComponent<ParticleSystem>();
            collisionEvents = new List<ParticleCollisionEvent>();
            particles = new ParticleSystem.Particle[ps.main.maxParticles];
        }

        void OnParticleCollision(GameObject other)
        {
            if (!other.CompareTag("Ground"))
                return;

            int eventCount = ps.GetCollisionEvents(other, collisionEvents);
            int count = ps.GetParticles(particles);

            for (int i = 0; i < eventCount; i++)
            {
                Vector3 collisionPos = collisionEvents[i].intersection;

                // Find the closest particle
                int closestIndex = -1;
                float closestDist = float.MaxValue;

                for (int j = 0; j < count; j++)
                {
                    float dist = Vector3.SqrMagnitude(particles[j].position - collisionPos);
                    if (dist < closestDist)
                    {
                        closestDist = dist;
                        closestIndex = j;
                    }
                }

                if (closestIndex != -1 && !stuckParticles.Contains(closestIndex))
                {
                    // Mark this particle as landed
                    stuckParticles.Add(closestIndex);

                    // Place it slightly above the ground
                    Vector3 groundPos = collisionPos + Vector3.up * 0.01f;
                    particles[closestIndex].position = groundPos;

                    // Zero out movement & rotation
                    particles[closestIndex].velocity = Vector3.zero;
                    particles[closestIndex].angularVelocity = 0f;
                    particles[closestIndex].rotation3D = new Vector3(90f, 0f, 0f);
                }
            }

            ps.SetParticles(particles, count);
        }

        void LateUpdate()
        {
            // Every frame, re-freeze any landed particles
            int count = ps.GetParticles(particles);
            foreach (int index in stuckParticles)
            {
                if (index < count)
                {
                    particles[index].velocity = Vector3.zero;
                    particles[index].angularVelocity = 0f;
                }
            }
            ps.SetParticles(particles, count);
        }
    }
}
