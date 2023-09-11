using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public GameObject enemyBulletPrefab;
    public Transform firePoint;

    private float timer;
    private void Update() {
        timer += Time.deltaTime;
        if(timer >= 1f){
            timer = 0f;
            tampilBullet();
        }
    }

    private void tampilBullet(){
        Instantiate(enemyBulletPrefab, firePoint.position, Quaternion.identity);
    }
}
