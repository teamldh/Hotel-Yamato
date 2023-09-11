using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPrefabs : MonoBehaviour
{
    public float bulletSpeed = 10f;
    //public float bulletLifetime = 1.5f;
    public int damageAmount = 5;
    public string targetTag;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * bulletSpeed * Time.deltaTime);

        timer += Time.deltaTime;
        if(timer > 2f){
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            collision.GetComponent<healthSystemPlayer>().TakeDamage(damageAmount);
            Destroy(gameObject);
        }
    }
}
