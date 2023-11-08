using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgmManager : MonoBehaviour
{
    public static bgmManager instance;

    [SerializeField] private AudioClip[] bgmClips;
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

    public void main_menu(){
        audioSource.clip = bgmClips[0];
        audioSource.loop = true;
        audioSource.Play();
    }

    public void kantor(){
        audioSource.clip = bgmClips[1];
        audioSource.loop = true;
        audioSource.Play();
    }

    public void kota(){
        audioSource.clip = bgmClips[2];
        audioSource.loop = true;
        audioSource.Play();
    }

    public void atapHotel(){
        audioSource.clip = bgmClips[3];
        audioSource.loop = true;
        audioSource.Play();
    }

    public void pertempuran(){
        audioSource.clip = bgmClips[4];
        audioSource.loop = true;
        audioSource.Play();
    }
}
