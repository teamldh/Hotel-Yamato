using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletleft : MonoBehaviour
{
    public float bulletSpeed = 10f;
    //public float bulletLifetime = 2f;
    public int damageAmount = 50;
    //public string targetTag;
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
}
