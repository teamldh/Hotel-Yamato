using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    [Header("Components Player")]
    public PlayerMovement PlayerMovement;
    public Animator anim;

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

        UpdateAnimation();
    }

    private void UpdateAnimation(){
        if(movement != Vector2.zero){
            anim.SetFloat("Horizontal", movement.x);
            anim.SetFloat("Vertical", movement.y);
            anim.SetBool("isWalking", true);
        }
        else{
            anim.SetBool("isWalking", false);
        }
    }
}
