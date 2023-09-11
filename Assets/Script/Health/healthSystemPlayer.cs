using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthSystemPlayer : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;

    public healthUI healthUI;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthUI.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;
        healthUI.SetHealth(currentHealth);
        if(currentHealth <= 0){
            Destroy(gameObject);
        }
    }
}
