using System;
using Boids;
using Climate;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities;
using Timer = Utilities.Timer;

namespace Creatures.Ducken
{
    public class Ducken : Animal, IBarkable
    {
        [Header("Ducken")]
        public Form currForm;

        public Food food;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float checkDistance;
        [SerializeField] private Chance soundChance;

        private TempReceptor tempReceptor;
        private Timer tempThrottle;
        [HideInInspector] public Boid boid;
        [HideInInspector] public DuckenData duckenData;
        private DuckenGUI gui;
        
        protected override void Awake() {
            base.Awake();
            duckenData = data as DuckenData;

            if (duckenData == null) {
                Debug.LogWarning("Ducken Data was not a Ducken Data");
                return;
            }
            
            boid = GetComponent<Boid>();
            tempReceptor = GetComponent<TempReceptor>();
            tempReceptor.onTempChange.AddListener(FormCheck);
            gui = GetComponent<DuckenGUI>();
        }

        protected override void Start() {
            base.Start();
            FormCheck();
        }

        private void FixedUpdate() {
            if (!IsGrounded() && rb.linearVelocity.y <= 0) {
                rb.AddForce(Vector3.down * duckenData.gravityForce, ForceMode.Acceleration);
            }
        }

        public virtual void BarkedAt(Transform sourcePosition) {
            if (soundChance.Roll()) {
                switch (currForm) {
                    case Form.Ducken:
                        emitter.EventReference = fmodEvents.duckenSound;
                        break;
                    case Form.Chicken:
                        emitter.EventReference = fmodEvents.chickenSound;
                        break;
                    case Form.Duck:
                        emitter.EventReference = fmodEvents.duckSound;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                emitter.Play();
            }
        }

        private void FormCheck() {
            if (tempReceptor.currTemp > duckenData.duckenThresh.max) {
                currForm = Form.Chicken;
            }
            else if (tempReceptor.currTemp < duckenData.duckenThresh.min) {
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
            tempReceptor.onTempChange.RemoveListener(FormCheck);
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