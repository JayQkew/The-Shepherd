using System;
using Boids;
using Climate;
using HerdingSystem;
using UnityEngine;
using Utilities;

namespace Creatures.Ducken
{
    public class Ducken : Animal, IBarkable
    {
        [Header("Ducken")]
        public Form currForm;
        public DuckenStats stats;

        private TempReceptor tempReceptor;
        private Timer tempThrottle;
        [HideInInspector] public Boid boid;


        protected override void Awake() {
            base.Awake();
            tempReceptor = GetComponent<TempReceptor>();
            tempReceptor.onCalcTemp.AddListener(FormCheck);
        }
        public virtual void BarkedAt(Vector3 sourcePosition) {}

        private void FormCheck() {
            if (tempReceptor.currTemp > stats.duckenThresh.max) {
                currForm = Form.Chicken;
            } 
            else if (tempReceptor.currTemp < stats.duckenThresh.min) {
                currForm = Form.Duck;
            }
            else {
                currForm = Form.Ducken;
            }
        }

        private void OnDestroy() {
            tempReceptor.onCalcTemp.RemoveListener(FormCheck);
        }
    }

    public enum Form
    {
        Ducken,
        Chicken,
        Duck
    }
}
