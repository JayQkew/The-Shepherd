using System;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    public Rigidbody rb;
    private InputHandler _inputHandler;

    [Header("Movement Stats")]
    [SerializeField] private float speed;
    [SerializeField] private float acceleration;
    [SerializeField] private float jumpForce;
    private Vector3 _desiredVelocity;
    
    [Header("Ground Check")]
    [SerializeField] private bool isGrounded;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float checkDistance;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        _inputHandler = GetComponent<InputHandler>();
    }

    private void Update() {
        _desiredVelocity = _inputHandler.move * speed;
        isGrounded = IsGrounded();
    }

    private void FixedUpdate() {
        Vector3 currVel = rb.linearVelocity;
        
        float xVel = Mathf.Lerp(currVel.x, _desiredVelocity.x, acceleration * Time.fixedDeltaTime);
        float zVel = Mathf.Lerp(currVel.z, _desiredVelocity.z, acceleration * Time.fixedDeltaTime);
        
        rb.linearVelocity = new Vector3(xVel, rb.linearVelocity.y, zVel);
    }

    private bool IsGrounded() {
        return Physics.Raycast(transform.position, Vector3.down, checkDistance, groundLayer);
    }
}

