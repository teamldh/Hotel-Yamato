using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPrefabs : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public float bulletLifetime = 2f;
    public int damageAmount = 50;
    public string targetTag;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, bulletLifetime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            collision.GetComponent<healthSystem>().TakeDamage(damageAmount);
            Destroy(gameObject);
        }
    }
}
