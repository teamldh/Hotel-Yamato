using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float mvSpeed;
    private Rigidbody2D rb;
    Vector2 movement;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); // -1, 0, 1
        movement.y = Input.GetAxisRaw("Vertical"); // -1, 0, 1
    }
    
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * mvSpeed * Time.fixedDeltaTime);
    }
}
