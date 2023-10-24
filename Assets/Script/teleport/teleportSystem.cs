using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportSystem : MonoBehaviour
{
    public Transform teleportTarget;
    private GameObject player;
    private bool interact;
    public GameObject hintInteract;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update() {
        if(interact){
            hintInteract.SetActive(true);
            if(PlayerControllerInputSystem.GetInstance().GetInteractInput()){
                teleport();
            }
        }
        else{
            hintInteract.SetActive(false);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            interact = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")){
            interact = false;
        }
    }

    private void teleport(){
        player.transform.position = teleportTarget.transform.position;
    }
}
