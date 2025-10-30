using System;
using System.ComponentModel;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace Creatures.Ducken
{
    [Serializable]
    public class DuckenChill : DuckenBaseState
    {
        [SerializeField] private Action currAction;
        public Timer timer;

        private float noiseSeed;
        private float currSpeed;
        private Vector3 targetDir;
        private Transform target;

        public override void EnterState(DuckenManager manager) {
            manager.boid.activeBoids = false;
            PickAction(manager);
        }

        public override void UpdateState(DuckenManager manager) {
            timer.Update();
            if (timer.IsFinished) {
                target = null;
                PickAction(manager);
            }
            else {
                if (currAction == Action.Roam) {
                    Vector3 roamDir = RoamDirection(1f);
                    targetDir = Vector3.Lerp(targetDir, roamDir, Time.deltaTime * 1.5f);
                    Vector3 move = targetDir * currSpeed;
                    manager.rb.linearVelocity = Vector3.Lerp(manager.rb.linearVelocity, move, Time.deltaTime * 3f);
                }
                else if (currAction == Action.Follow && target != null) {
                    targetDir = (target.position - manager.transform.position).normalized;
                    Vector3 move = targetDir * currSpeed;

                    float distance = Vector3.Distance(manager.transform.position, target.position);
                    if (distance < 0.5f) {
                        manager.rb.linearVelocity = Vector3.Lerp(manager.rb.linearVelocity, move, Time.deltaTime * 3f);
                    }
                }
            }
        }

        public override void ExitState(DuckenManager manager) {
        }

        private Vector3 RoamDirection(float perlinScale) {
            float x = Mathf.PerlinNoise(noiseSeed, Time.time * perlinScale) * 2f - 1f;
            float z = Mathf.PerlinNoise(noiseSeed + 10f, Time.time * perlinScale) * 2f - 1f;
            return new Vector3(x, 0, z).normalized;
        }

        private void PickAction(DuckenManager manager) {
            Action newAction;

            if (target != null) {
                newAction = Action.Follow;
            }
            else {
                Action[] actions = (Action[])Enum.GetValues(typeof(Action));

                do {
                    newAction = actions[Random.Range(0, actions.Length)];
                } while (newAction == currAction || newAction == Action.Follow);
            }

            switch (newAction) {
                case Action.Idle:
                    timer.SetMaxTime(manager.duckenData.idleTime.RandomValue());
                    break;
                case Action.Sleep:
                    timer.SetMaxTime(manager.duckenData.sleepTime.RandomValue());
                    break;
                case Action.Eat:
                    timer.SetMaxTime(manager.duckenData.eatTime.RandomValue());
                    manager.food.Eat();
                    break;
                case Action.Roam:
                    timer.SetMaxTime(manager.duckenData.roamTime.RandomValue());
                    noiseSeed = Random.value * 100f;
                    currSpeed = manager.duckenData.roamSpeed.RandomValue();
                    manager.emitter.EventReference = manager.fmodEvents.duckenWalk;
                    manager.emitter.Play();
                    break;
                case Action.Follow:
                    timer.SetMaxTime(manager.duckenData.followTime.RandomValue());
                    currSpeed = manager.duckenData.roamSpeed.RandomValue();
                    manager.emitter.EventReference = manager.fmodEvents.duckenWalk;
                    manager.emitter.Play();
                    break;
            }

            currAction = newAction;
        }

        public void Target(Transform t) => target = t;

        public enum Action
        {
            [Description("Idle")]
            Idle,
            [Description("Sleep")]
            Sleep,
            [Description("Eat")]
            Eat,
            [Description("Idle")]
            Roam,
            Follow
        }
    }
}