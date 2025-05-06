using System;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    private Rigidbody _rb;
    private InputHandler _inputHandler;

    [Header("Movement Stats")]
    [SerializeField] private float speed;
    [SerializeField] private float acceleration;
    [SerializeField] private float jumpForce;
    private Vector3 _desiredVelocity;
    
    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float checkDistance;

    private void Awake() {
        _rb = GetComponent<Rigidbody>();
        _inputHandler = GetComponent<InputHandler>();
    }

    private void Update() {
        _desiredVelocity = _inputHandler.move * speed;
    }

    private void FixedUpdate() {
        Vector3 currVel = _rb.linearVelocity;

        float xVel = Mathf.Lerp(currVel.x, _desiredVelocity.x, acceleration * Time.fixedDeltaTime);
        float zVel = Mathf.Lerp(currVel.z, _desiredVelocity.z, acceleration * Time.fixedDeltaTime);
        
        _rb.linearVelocity = new Vector3(xVel, _rb.linearVelocity.y, zVel);
    }

    public void Jump() {
        if (IsGrounded()) {
            _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, 0, _rb.linearVelocity.z);
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool IsGrounded() {
        return Physics.Raycast(transform.position, Vector3.down, checkDistance, groundLayer);
    }
}

