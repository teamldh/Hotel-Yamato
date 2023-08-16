using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class bendera : MonoBehaviour
{
    private bool IsInteractable;
    public GameObject interactHint;
    public Collider2D benderaCollider2D;
    public UnityEvent customEvent;
    public Mision_act1 misi;

    private void Awake() {
        IsInteractable = false;
        interactHint.SetActive(false);
    }
    private void Update() {
        if(IsInteractable){
            interactHint.SetActive(true);
            if(PlayerControllerInputSystem.GetInstance().GetInteractInput()){
                interact();
                Destroy(benderaCollider2D);
            }
        }
        else{
            interactHint.SetActive(false);
        }
    }

    private void interact(){
        customEvent.Invoke();
        misi.benderaCountUp();
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
