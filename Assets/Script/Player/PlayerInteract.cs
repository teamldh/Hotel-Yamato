using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float interactionRadius;
    public LayerMask interactableLayer;

    public void interact(Vector2 playerPosition){
        playerPosition = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(playerPosition, interactionRadius, interactableLayer);

        foreach (Collider2D collider in colliders)
        {
            // Jika ada objek yang dapat diinteraksi, lakukan tindakan sesuai kebutuhan
            // Contoh tindakan: print pesan ke konsol
            Debug.Log("Interacted with: " + collider.gameObject.name);

            // Contoh lain: jika ada script khusus pada objek interaktif yang ingin dieksekusi, panggil metode dari objek tersebut.
            // collider.gameObject.GetComponent<InteractableObjectScript>().Interact();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
