using System;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities;
using Random = UnityEngine.Random;

namespace Creatures.Ducken
{
    [Serializable]
    public class DuckenPanic : DuckenBaseState
    {
        public Timer timer;

        private float currSpeed;
        private Vector3 targetDir;
        private float noiseSeed;
        public override void EnterState(DuckenManager manager) {
            manager.boid.activeBoids = true;
            timer.SetMaxTime(manager.duckenData.runTime.RandomValue());
            currSpeed = manager.duckenData.roamSpeed.RandomValue() * 2f;
            noiseSeed = Random.value * 100f;
        }

        public override void UpdateState(DuckenManager manager) {
            timer.Update();
            if (timer.IsFinished) {
                manager.SwitchState(manager.duckenChill);
            }
            
            Vector3 roamDir = RoamDirection(0.3f);
            targetDir = Vector3.Lerp(targetDir, roamDir, Time.deltaTime * 3f);
            Vector3 move = targetDir * currSpeed;
            manager.rb.linearVelocity = Vector3.Lerp(manager.rb.linearVelocity, move, Time.deltaTime * 3f);
        }

        public override void ExitState(DuckenManager manager) {
        }
        
        private Vector3 RoamDirection(float perlinScale) {
            float x = Mathf.PerlinNoise(noiseSeed, Time.time * perlinScale) * 2f - 1f;
            float z = Mathf.PerlinNoise(noiseSeed + 10f, Time.time * perlinScale) * 2f - 1f;
            return new Vector3(x, 0, z).normalized;
        }
    }
}
