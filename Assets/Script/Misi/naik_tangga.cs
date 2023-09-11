using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class naik_tangga : MonoBehaviour
{
    private bool interact;
    public string sceneName;
    public GameObject hintInteract;
    
    private void Update() {
        if(interact){
            hintInteract.SetActive(true);
            if(PlayerControllerInputSystem.GetInstance().GetInteractInput()){
                loadScene();
            }
        }
        else{
            hintInteract.SetActive(false);
        }
    }

    private void loadScene(){
        SceneManager.LoadScene(sceneName);
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
}
