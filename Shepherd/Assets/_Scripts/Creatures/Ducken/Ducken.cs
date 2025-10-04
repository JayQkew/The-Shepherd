using System;
using Climate;
using HerdingSystem;
using UnityEngine;
using Utilities;

namespace Creatures.Ducken
{
    public class Ducken : HerdAnimal, IBarkable
    {
        [Header("Ducken")]
        public Form currForm;
        
        private DuckenStateManager duckenStateManager;
        private DuckenStats stats;
        private TempReceptor tempReceptor;
        private Timer tempThrottle;

        protected override void Awake() {
            base.Awake();
            duckenStateManager = GetComponent<DuckenStateManager>();
            stats = duckenStateManager.stats;
            tempReceptor = GetComponent<TempReceptor>();
            tempReceptor.onCalcTemp.AddListener(FormCheck);
        }
        public void BarkedAt(Vector3 sourcePosition) {
            switch (currForm) {
                case Form.Ducken:
                    //follow the source
                    break;
                case Form.Chicken:
                    //jump and run around frantically
                    break;
                case Form.Duck:
                    // freeze and turn into ice (slippery)
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

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
