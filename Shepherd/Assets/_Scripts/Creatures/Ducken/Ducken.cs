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
        public Food food;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float checkDistance;

        private TempReceptor tempReceptor;
        private Timer tempThrottle;
        [HideInInspector] public Boid boid;
        private DuckenGUI gui;


        protected override void Awake() {
            base.Awake();
            boid = GetComponent<Boid>();
            tempReceptor = GetComponent<TempReceptor>();
            tempReceptor.onCalcTemp.AddListener(FormCheck);
            gui = GetComponent<DuckenGUI>();
        }

        private void FixedUpdate() {
            if (!IsGrounded() && rb.linearVelocity.y <= 0) {
                rb.AddForce(Vector3.down * stats.gravityForce, ForceMode.Acceleration);
            }
        }

        public virtual void BarkedAt(Vector3 sourcePosition) { }

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
            
            gui.ChangeSprite(currForm);
        }
        
        public bool IsGrounded() {
            return Physics.Raycast(transform.position, Vector3.down, checkDistance, groundLayer);
        }

        private void OnDestroy() {
            tempReceptor.onCalcTemp.RemoveListener(FormCheck);
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * checkDistance);
        }
    }

    public enum Form
    {
        Ducken,
        Chicken,
        Duck
    }
}
