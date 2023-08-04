using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerInputSystem : MonoBehaviour
{
    public static PlayerControllerInputSystem instance;

    private PlayerInput playerInput;
    private InputActionMap playerController;
    
    //Variable used in this script
    Vector2 moveInput;
    bool attackInput;
    bool interactInput;

    private void Awake() {
        if(instance != null){
            Debug.LogError("Found more than one Input Manager in the scene.");
        }
        instance = this;

        playerInput = GetComponent<PlayerInput>();
        playerController = playerInput.actions.FindActionMap("PlayerController");
    }
    
    public void OnMove(InputAction.CallbackContext context) {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log(moveInput);
    }

    public void OnAttack(InputAction.CallbackContext context) {
        attackInput = context.performed;
        Debug.Log(attackInput);
    }

    public void OnInteract(InputAction.CallbackContext context){
        interactInput = context.performed;
        Debug.Log(interactInput);
    }

    //holdable button
    public Vector2 GetMoveInput() {
        return moveInput;
    }

    //button pressed once
    public bool GetAttackInput() {
        bool temp = attackInput;
        attackInput = false;
        return temp;
    }

    public bool GetInteractInput() {
        bool temp = interactInput;
        interactInput = false;
        return temp;
    }

    //enable/disable input player controller
    public void SetEnableInputMovement(bool value) {
        if(value) {
            playerController.Enable();
        } else {
            playerController.Disable();
        }
    }

}
