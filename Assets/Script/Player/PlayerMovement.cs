using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float mvSpeed;
    private Rigidbody2D rb;
    // Vector2 movement;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 movement)
    {
        rb.MovePosition(rb.position + movement * mvSpeed * Time.fixedDeltaTime);
    }
}
