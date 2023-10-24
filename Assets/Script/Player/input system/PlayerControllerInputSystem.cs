using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerInputSystem : MonoBehaviour
{
    private static PlayerControllerInputSystem instance;

    private PlayerInput playerInput;
    private InputActionMap playerController;
    private InputActionMap UiInput;
    
    //Variable used in this script
    Vector2 moveInput;
    Vector2 PointerInput;
    bool attackInput;
    bool interactInput;
    bool submitInput;
    bool pauseInput;
    bool resumeInput;

    private void Awake() {
        if(instance != null){
            Debug.LogError("Found more than one Input Manager in the scene.");
        }
        instance = this;

        playerInput = GetComponent<PlayerInput>();
        playerController = playerInput.actions.FindActionMap("PlayerController");
        UiInput = playerInput.actions.FindActionMap("UI");
    }
    public static PlayerControllerInputSystem GetInstance() 
    {
        return instance;
    }
    
    public void OnMove(InputAction.CallbackContext context) {
        moveInput = context.ReadValue<Vector2>();
        //Debug.Log(moveInput);
    }

    public void OnPointer(InputAction.CallbackContext context){
        PointerInput = context.ReadValue<Vector2>();
        //Debug.Log(PointerInput);
    }

    public void OnAttack(InputAction.CallbackContext context) {
        attackInput = context.performed;
        //Debug.Log(attackInput);
    }

    public void OnInteract(InputAction.CallbackContext context){
        interactInput = context.performed;
        //Debug.Log(interactInput);
    }

    public void OnSubmitPress(InputAction.CallbackContext context){
        submitInput = context.performed;
        //Debug.Log(submitInput);
    }

    public void OnPause(InputAction.CallbackContext context){
        pauseInput = context.performed;
        //Debug.Log(pauseInput);
    }

    public void OnResume(InputAction.CallbackContext context){
        resumeInput = context.performed;
        //Debug.Log(resumeInput);
    }

    //holdable button
    public Vector2 GetMoveInput() {
        return moveInput;
    }
    public bool GetAttackInput() {
        return attackInput;
    }

    //button pressed once
    public bool GetInteractInput() {
        bool temp = interactInput;
        interactInput = false;
        return temp;
    }

    public bool GetSubmitPress(){
        bool temp = submitInput;
        submitInput = false;
        return temp;
    }

    public bool GetPauseInput(){
        bool temp = pauseInput;
        pauseInput = false;
        return temp;
    }

    public bool GetResumeInput(){
        bool temp = resumeInput;
        resumeInput = false;
        return temp;
    }

    public void RegisterSubmitPressed() 
    {
        submitInput = false;
    }

    //enable/disable input player controller
    public void SetEnableInputMovement(bool value) {
        if(value) {
            playerController.Enable();
        } else {
            playerController.Disable();
        }
    }

    public void SetEnableInputUI(bool value) {
        if(value) {
            UiInput.Enable();
        } else {
            UiInput.Disable();
        }
    }

}
