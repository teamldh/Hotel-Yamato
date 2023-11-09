using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfxManager : MonoBehaviour
{
    public static sfxManager instance;

    [SerializeField] private AudioClip[] sfxClips;
    public AudioSource audioSource { get; private set; }

    private void Awake() {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
        audioSource = GetComponent<AudioSource>();
    }

    public void sfx_1(){
        audioSource.PlayOneShot(sfxClips[0]);
    }

    public void sfx_2(){
        audioSource.PlayOneShot(sfxClips[1]);
    }

    public void sfx_3(){
        audioSource.PlayOneShot(sfxClips[2]);
    }

    public void weaponShot(){
        audioSource.PlayOneShot(sfxClips[4]);
    }
}
