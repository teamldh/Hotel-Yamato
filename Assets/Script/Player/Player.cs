using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Components Player")]
    public PlayerMovement PlayerMovement;
    public Animator animator;

    // Variable private
    private Vector2 movement;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement = GetComponent<PlayerMovement>();
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = PlayerControllerInputSystem.GetInstance().GetMoveInput().normalized;
        animator.SetFloat("jalan", Mathf.Abs(movement.x));

        if(movement.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if(movement.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void FixedUpdate()
    {
        PlayerMovement.Move(movement);
    }
}
