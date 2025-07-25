using System;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    private Rigidbody rb;
    private InputHandler inputHandler;

    [Header("Movement Stats")]
    [SerializeField] private float maxSpeed;

    [SerializeField] private float sprintMaxSpeed;
    [SerializeField] private float acceleration;
    private Vector3 desiredVelocity;

    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float checkDistance;
    public bool grounded;

    [Header("Jump")]
    [SerializeField] private float jumpForce;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        inputHandler = GetComponent<InputHandler>();
    }

    private void Update() {
        float mult = inputHandler.isSprinting ? sprintMaxSpeed : maxSpeed;
        desiredVelocity = inputHandler.move * mult;

        grounded = IsGrounded();
    }

    private void FixedUpdate() {
        Move();
        JumpTrajectory();
    }

    private void Move() {
        Vector3 currVel = rb.linearVelocity;

        float xVel = Mathf.Lerp(currVel.x, desiredVelocity.x, acceleration * Time.fixedDeltaTime);
        float zVel = Mathf.Lerp(currVel.z, desiredVelocity.z, acceleration * Time.fixedDeltaTime);

        rb.linearVelocity = new Vector3(xVel, rb.linearVelocity.y, zVel);
    }

    private void JumpTrajectory() {
        if (!grounded) {
            float mult = rb.linearVelocity.y <= 0 ? jumpForce * 2 : jumpForce;
            rb.AddForce(Vector3.down * mult, ForceMode.Force);
        }
    }

    public void Jump() {
        if (grounded) {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool IsGrounded() {
        return Physics.Raycast(transform.position, Vector3.down, checkDistance, groundLayer);
    }
}