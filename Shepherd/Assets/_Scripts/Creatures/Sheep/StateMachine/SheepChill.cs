using System;
using System.ComponentModel;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace Creatures.Sheep
{
    [Serializable]
    public class SheepChill : SheepBaseState
    {
        [SerializeField] private Action currAction;
        public Timer timer;

        private float noiseSeed;
        private float currSpeed;
        private Vector3 targetDir;
        public override void EnterState(SheepManager manager) {
            manager.boid.activeBoids = false;
            PickAction(manager);
        }

        public override void UpdateState(SheepManager manager) {
            timer.Update();
            if (currAction is Action.Idle or Action.Roam)manager.gui.UpdateSuppAnims();
            if (timer.IsFinished) PickAction(manager);
            
            if (currAction != Action.Roam) return;

            Vector3 roamDir = RoamDirection(1f);
            targetDir = Vector3.Lerp(targetDir,roamDir, Time.deltaTime * 0.5f);
            Vector3 move = targetDir * currSpeed;
            manager.rb.linearVelocity = Vector3.Lerp(manager.rb.linearVelocity, move, Time.deltaTime * 3f);
        }

        public override void ExitState(SheepManager manager) {
        }

        private Vector3 RoamDirection(float perlinScale) {
            float x = Mathf.PerlinNoise(noiseSeed, Time.time * perlinScale) * 2f - 1f;
            float z = Mathf.PerlinNoise(noiseSeed + 10f, Time.time * perlinScale) * 2f - 1f;
            return new Vector3(x, 0, z).normalized;
        }

        private void PickAction(SheepManager manager) {
            Action[] actions = (Action[])Enum.GetValues(typeof(Action));
            Action newAction;
            do {
                newAction = actions[Random.Range(0, actions.Length)];
            } while (newAction == currAction);
            
            manager.gui.PlayAnim(newAction.StringValue());
            switch (newAction) {
                case Action.Idle:
                    timer.SetMaxTime(manager.sheepData.idleTime.RandomValue());
                    break;
                case Action.Sleep:
                    timer.SetMaxTime(manager.sheepData.sleepTime.RandomValue());
                    break;
                case Action.Eat:
                    timer.SetMaxTime(manager.sheepData.eatTime.RandomValue());
                    manager.food.Eat();
                    manager.emitter.EventReference = manager.fmodEvents.sheepEat;
                    manager.emitter.Play();
                    break;
                case Action.Roam:
                    timer.SetMaxTime(manager.sheepData.roamTime.RandomValue());
                    noiseSeed = Random.value * 100f;
                    currSpeed = manager.sheepData.roamSpeed.RandomValue();
                    break;
            }
            
            currAction = newAction;
        }

        public enum Action
        {
            [Description("Idle")]
            Idle,
            [Description("Sleep")]
            Sleep,
            [Description("Eat")]
            Eat,
            [Description("Idle")]
            Roam
        }
    }
}
