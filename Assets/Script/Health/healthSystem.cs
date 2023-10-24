using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthSystem : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth {get; set;}

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage){
        currentHealth -= damage;
        if(currentHealth <= 0){
            if(gameObject.tag == "Enemy"){
                gameObject.GetComponent<enemy>().dropHealth();
                //gameObject.GetComponent<countEnemyDead>().countEnemy++;
                //GameObject.Find("NPC_hero").GetComponent<countEnemyDead>().countEnemy++;
                Destroy(gameObject);
            }
            if(gameObject.tag == "Player"){
                Debug.Log("Game Over");
                GameObject.Find("manager").GetComponent<gameOver>().gameOverPanel();

            }
        }
    }

    public void Heal(float healAmount){
        currentHealth += healAmount;
        if(currentHealth > maxHealth){
            currentHealth = maxHealth;
        }
    }
}
