using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupHealth : MonoBehaviour
{
    public float healthAmount;
    private healthSystem healthSystem;
    private void Start() {
        healthSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<healthSystem>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            healthSystem.Heal(healthAmount);
            Destroy(gameObject);
        }
    }
}
