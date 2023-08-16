using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float interactionRadius;
    public GameObject hintPrefab;
    public LayerMask interactableLayer;

    private GameObject indicatorObject;
    private GameObject objectInteract;


    private void FixedUpdate() {
        detectionObjectInteract();

        if(objectInteract != null && PlayerControllerInputSystem.GetInstance().GetInteractInput()){
            if(objectInteract.GetComponent<interactable>() != null){
                HideHint();
                objectInteract.GetComponent<interactable>().interact();
            }
            
            
        }
    }
    
    private void detectionObjectInteract(){
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactionRadius, interactableLayer);
        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D collider in colliders)
        {
            if(collider.GetComponent<interactable>() != null){
                
                if(!collider.GetComponent<interactable>().IsInteractable){
                    break;
                }

                //mendeteksi objek yang paling dekat dengan player
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestObject = collider.gameObject;
                }
            }
        }

        //jika ada objek yang bisa diinteract didekat player
        if(closestObject != null){
            if(indicatorObject == null || closestObject != objectInteract && hintPrefab != null){
                HideHint();
                float yOffset = (closestObject.GetComponent<Collider2D>().bounds.size.y / 2f) + 1f;
                indicatorObject = Instantiate(hintPrefab, closestObject.transform.position + Vector3.up * yOffset, Quaternion.identity);
            }
        }

        //menyimpan objek yang bisa diinteract ke dalam variabel objectInteract
        objectInteract = closestObject;

        //jika tidak ada objek yang bisa diinteract didekat player
        if(objectInteract == null){
            HideHint();
        }
    }

    public void HideHint()
    {
        if (indicatorObject != null)
        {
            Destroy(indicatorObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
