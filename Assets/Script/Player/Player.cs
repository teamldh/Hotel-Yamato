using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Components Player")]
    public PlayerMovement PlayerMovement;
    public PlayerInteract PlayerInteract;

    // Variable private
    private Vector2 movement;
    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement = GetComponent<PlayerMovement>();
        PlayerInteract = GetComponent<PlayerInteract>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); // -1, 0, 1
        movement.y = Input.GetAxisRaw("Vertical"); // -1, 0, 1
        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayerInteract.interact(movement);
        }
    }

    void FixedUpdate()
    {
        PlayerMovement.Move(movement);
    }
}
