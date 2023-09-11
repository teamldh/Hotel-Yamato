using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tesSpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float respawnTime = 5.0f;

    private float timer = 0.0f;

    void Update()
    {
        if (transform.childCount == 0)
        {
            timer += Time.deltaTime;
            
            if (timer >= respawnTime)
            {
                SpawnEnemy();
                timer = 0.0f;
            }
        }
    }

    void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        newEnemy.transform.parent = transform; // Set the spawn point as the parent
    }
}
