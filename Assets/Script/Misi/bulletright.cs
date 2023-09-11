using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletright : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public int damageAmount = 50;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frames
    void Update()
    {
        transform.Translate(Vector2.right * bulletSpeed * Time.deltaTime);

        timer += Time.deltaTime;
        if(timer > 2f){
            Destroy(gameObject);
        }
    }
}
