using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void Update() {
        switch(SceneManager.GetActiveScene().name){
            case "Begining":
                noBGM();
                break;
            case "Lapangan1":
                noBGM();
                break;
            case "OutdoorHotelYamato":
                noBGM();
                break;
            case "OutdoorHotelYamato1":
                noBGM();
                break;
            case "IndorHotelYamatoPerundingan":
                noBGM();
                break;
        }
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

    public void noBGM(){
        audioSource.Stop();
    }
}
