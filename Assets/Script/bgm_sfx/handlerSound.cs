using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class handlerSound : MonoBehaviour
{
    [SerializeField] private Slider slider_BGM;
    [SerializeField] private Slider slider_SFX;
    private void Start() {
        slider_BGM.value = bgmManager.instance.audioSource.volume;
        slider_SFX.value = sfxManager.instance.audioSource.volume;
        changeBGM();
    }

    public void volumeSlider_BGM(){
        bgmManager.instance.audioSource.volume = slider_BGM.value;
    }

    public void volumeSlider_SFX(){
        sfxManager.instance.audioSource.volume = slider_SFX.value;
    }

    private void changeBGM(){
        switch(SceneManager.GetActiveScene().name){
            case "MainMenu":
                bgmManager.instance.main_menu();
                break;
            case "Kantor":
                bgmManager.instance.kantor();
                break;
            case "Kota":
                bgmManager.instance.kota();
                break;
            case "RoofHotelYamato":
                bgmManager.instance.atapHotel();
                break;
            case "IndorHotelYamatoPertempuran": 
                bgmManager.instance.pertempuran();
                break;
            case "RoofHotelYamato2":
                bgmManager.instance.pertempuran();
                break;
        }
    }
}
