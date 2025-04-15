using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public Vector2 move;
    public Vector2 aim;
    [SerializeField] private bool isJumping;
    [SerializeField] private bool isCrouching;
    [SerializeField] private bool isSprinting;
    [SerializeField] private bool isBarking;
    [SerializeField] private bool isInteracting;
    
    [Header("Events")]
    public UnityEvent OnJump;
    [Space(25)]
    public UnityEvent OnCrouch;
    [Space(25)]
    public UnityEvent OnSprint;
    [Space(25)]
    public UnityEvent OnBark;
    [Space(25)]
    public UnityEvent OnInteract;
    
    public void Move(InputAction.CallbackContext ctx) => move = ctx.ReadValue<Vector2>();

    public void Aim(InputAction.CallbackContext ctx) => aim = Camera.main.ScreenToWorldPoint(ctx.ReadValue<Vector2>());

    public void Jump(InputAction.CallbackContext ctx) {
        if (ctx.performed) {
            OnJump?.Invoke();
            isJumping = true;
        } else if (ctx.canceled) {
            isJumping = false;
        }
    }

    public void Crouch(InputAction.CallbackContext ctx) {
        if (ctx.performed) {
            OnCrouch?.Invoke();
            isCrouching = true;
        } else if (ctx.canceled) {
            isCrouching = false;
        }
    }

    public void Sprint(InputAction.CallbackContext ctx) {
        if (ctx.performed) {
            OnSprint?.Invoke();
            isSprinting = true;
        } else if (ctx.canceled) {
            isSprinting = false;
        }
    }

    public void Bark(InputAction.CallbackContext ctx) {
        if (ctx.performed) {
            OnBark?.Invoke();
            isBarking = true;
        } else if (ctx.canceled) {
            isBarking = false;
        }
    }

    public void Interact(InputAction.CallbackContext ctx) {
        if (ctx.performed) {
            OnInteract?.Invoke();
            isInteracting = true;
        } else if (ctx.canceled) {
            isInteracting = false;
        }
    }

}
