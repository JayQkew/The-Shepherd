using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(PlayerInput))]
public class InputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    
    public Vector3 move;
    public Vector3 aim;
    public Vector2 zoom;
    [SerializeField] private bool isJumping;
    [SerializeField] private bool isCrouching;
    public bool isSprinting;
    [SerializeField] private bool isBarking;
    [SerializeField] private bool isInteracting;
    [SerializeField] private bool isCasting;
    [Space(10)]
    [SerializeField] private Camera cam;

    [Header("Events")]
    public UnityEvent onJump;
    [Space(25)]
    public UnityEvent onCrouch;
    [Space(25)]
    public UnityEvent onSprint;
    [Space(25)]
    public UnityEvent onBark;
    [Space(25)]
    public UnityEvent onInteract;
    [Space(25)]
    public UnityEvent onCast;

    private void Start() {
        playerInput = GetComponent<PlayerInput>();
    }

    public void Move(InputAction.CallbackContext ctx) {
        Vector2 input = ctx.ReadValue<Vector2>();
        move = new Vector3(input.x, 0, input.y);
    }

    public void Aim(InputAction.CallbackContext ctx) {

        if (playerInput.currentControlScheme == "Keyboard&Mouse") {
            Vector2 mousePos = ctx.ReadValue<Vector2>();
            Ray ray = cam.ScreenPointToRay(mousePos);
            Physics.Raycast(ray, out RaycastHit hit);
            Vector3 hitPoint = hit.point;
            Vector3 playerPos = transform.position;
            Vector3 direction = hitPoint - playerPos;
            aim = new Vector3(direction.normalized.x, 0f, direction.normalized.z);
        }
        else {
            Vector2 aimDir = ctx.ReadValue<Vector2>();
            aim = new Vector3(aimDir.x, 0f, aimDir.y);
        }
        
    }

    public void Zoom(InputAction.CallbackContext ctx) => zoom = ctx.ReadValue<Vector2>();

    public void Jump(InputAction.CallbackContext ctx) {
        if (ctx.performed) {
            onJump?.Invoke();
            isJumping = true;
        }
        else if (ctx.canceled) {
            isJumping = false;
        }
    }

    public void Crouch(InputAction.CallbackContext ctx) {
        if (ctx.performed) {
            onCrouch?.Invoke();
            isCrouching = true;
        }
        else if (ctx.canceled) {
            isCrouching = false;
        }
    }

    public void Sprint(InputAction.CallbackContext ctx) {
        if (ctx.performed) {
            onSprint?.Invoke();
            isSprinting = true;
        }
        else if (ctx.canceled) {
            isSprinting = false;
        }
    }

    public void Bark(InputAction.CallbackContext ctx) {
        if (ctx.performed) {
            onBark?.Invoke();
            isBarking = true;
        }
        else if (ctx.canceled) {
            isBarking = false;
        }
    }

    public void Interact(InputAction.CallbackContext ctx) {
        if (ctx.performed) {
            onInteract?.Invoke();
            isInteracting = true;
        }
        else if (ctx.canceled) {
            isInteracting = false;
        }
    }

    public void Cast(InputAction.CallbackContext ctx) {
        if (ctx.performed) {
            onCast?.Invoke();
            isCasting = true;
        }
        else if (ctx.canceled) {
            isCasting = false;
        }
    }
}