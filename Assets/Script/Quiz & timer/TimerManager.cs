using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static TimerManager instance;
    private float timerCount;
    private bool isTimerRunning;

    private void Awake()
    {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isTimerRunning){
            timerCount += Time.deltaTime;
        }
    }

    public void startimer(){
        isTimerRunning = true;
    }

    public void stopTimer(){
        isTimerRunning = false;
    }

    public void resetTimer(){
        timerCount = 0;
    }

    public float getTimerCount(){
        return timerCount;
    }
}
