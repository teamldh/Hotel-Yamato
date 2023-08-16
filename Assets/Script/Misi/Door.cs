using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    private bool IsInteractable;
    public GameObject interactHint;
    //public PlayableDirector playableDirector;
    public UnityEvent customEvent;

     private void Update() {
        if(IsInteractable){
            interactHint.SetActive(true);
            if(PlayerControllerInputSystem.GetInstance().GetInteractInput()){
                interact();
            }
        }
        else{
            interactHint.SetActive(false);
        }
    }
    private void interact(){
        //PlayerControllerInputSystem.GetInstance().SetEnableInputMovement(false);
        //PlayerControllerInputSystem.GetInstance().SetEnableInputUI(false);
        //playableDirector.Play();
        customEvent.Invoke();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            IsInteractable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")){
            IsInteractable = false;
        }
    }
}
