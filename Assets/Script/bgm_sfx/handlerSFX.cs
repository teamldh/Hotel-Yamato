using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class handlerSFX : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Button btntest;
    private void Start() {
        slider.value = sfxManager.instance.audioSource.volume;
    }

    public void volumeSlider(){
        sfxManager.instance.audioSource.volume = slider.value;
    }

    // public void test_1(){
    //     sfxManager.instance.audio_1();
    // }
    // public void test_2(){
    //     sfxManager.instance.audio_2();
    // }
}
