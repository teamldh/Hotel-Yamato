using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCshoot : MonoBehaviour
{
    public GameObject enemyBulletPrefab;
    public Transform firePoint;
    public float damage;
    public float speedBullet;
    public string tagCollide;
    public float fireRate;
    private float timer;
    private void Update() {
        timer += Time.deltaTime;
        if(timer >= fireRate){
            timer = 0f;
            tampilBullet();
        }
    }

    private void tampilBullet(){
        GameObject bullet = Instantiate(enemyBulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Bullet>().damage = damage;
        bullet.GetComponent<Bullet>().bulletSpeed = speedBullet;
        bullet.GetComponent<Bullet>().tagCollide = tagCollide;
        bullet.GetComponent<SpriteRenderer>().color = Color.green;
    }

    public void hentikanTembakan(){
        this.GetComponent<NPCshoot>().enabled = false;
    }
    public void mulaiTembakan(){
        this.GetComponent<NPCshoot>().enabled = true;
    }
}
