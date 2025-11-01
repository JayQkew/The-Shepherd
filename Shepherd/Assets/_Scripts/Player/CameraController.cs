using System;
using Unity.Cinemachine;
using UnityEngine;
using Utilities;

namespace Player
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineFollow cinemachineFollow;
        [SerializeField] private float movingThresh;

        [Header("Camera Offsets")]
        [SerializeField] private Vector3 chillOffset;
        [SerializeField] private Vector3 walkOffset;
        [SerializeField] private Vector3 sprintOffset;

        [Header("Timing")]
        [SerializeField] private float smoothTime;
        [SerializeField] private Timer idleTimer;
        [SerializeField] private Timer sprintTimer;

        private Movement movement;
        private Rigidbody rb;
        private Vector3 currentVelocity;
        private Vector3 targetOffset;

        private bool wasSprinting;

        private void Awake() {
            movement = GetComponent<Movement>();
            rb = GetComponent<Rigidbody>();
            idleTimer.currTime = idleTimer.maxTime;
        }

        private void Update() {
            UpdateFollowOffset();
        }

        private void UpdateFollowOffset() {
            float speed = rb.linearVelocity.magnitude;
            bool isMoving = speed > movingThresh;
            bool isSprinting = movement.IsSprinting;

            if (isSprinting) {
                targetOffset = sprintOffset;
                sprintTimer.Reset();
                idleTimer.Reset();
                wasSprinting = true;
            }
            else {
                if (wasSprinting) {
                    sprintTimer.Update();

                    if (!sprintTimer.IsFinished) {
                        targetOffset = sprintOffset;
                        return;
                    }
                    wasSprinting = false;
                }

                if (isMoving) {
                    targetOffset = walkOffset;
                    idleTimer.Reset();
                }
                else {
                    idleTimer.Update();
                    if (idleTimer.IsFinished) {
                        targetOffset = chillOffset;
                    }
                }
            }

            cinemachineFollow.FollowOffset = Vector3.SmoothDamp(
                cinemachineFollow.FollowOffset,
                targetOffset,
                ref currentVelocity,
                smoothTime
            );
        }
    }
}