using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopwatch : MonoBehaviour
{
    bool stopWatch = false;
    float currenttime;

    // Start is called before the first frame update
    void Start()
    {
        currenttime = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        if (stopWatch)
        {
            currenttime += Time.deltaTime;
        }
    }
}
