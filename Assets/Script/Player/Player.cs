using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Components Player")]
    public PlayerMovement PlayerMovement;

    // Variable private
    private Vector2 movement;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = PlayerControllerInputSystem.GetInstance().GetMoveInput().normalized;
    }

    void FixedUpdate()
    {
        PlayerMovement.Move(movement);
    }
}
