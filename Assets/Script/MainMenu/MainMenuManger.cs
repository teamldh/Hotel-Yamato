using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuManger : MonoBehaviour
{
    [Header("Main Menu UI")]
    public GameObject mainMenuUI;
    public GameObject aboutMenuUI;
    public GameObject settingMenuUI;
    public GameObject playButton;
    public GameObject aboutButton;
    public GameObject settingButton;
    public GameObject exitButton;
    public GameObject backButton;
    public Button continueButton;

    // Start is called before the first frame update
    void Start()
    {
        Panelmainmenu();

        GameData data = SaveManager.instance.LoadSceneData();
        if (data != null)
        {
            continueButton.interactable = true;
        }
        else
        {
            continueButton.interactable = false;
        }
        
    }

    public void playGame(){
        sfxManager.instance.sfx_2();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void continueGame(){
        sfxManager.instance.sfx_3();
        LoadSceneData();
    }

    private void LoadSceneData()
    {
        GameData data = SaveManager.instance.LoadSceneData();
        if (data != null)
        {
            SceneManager.LoadScene(data.sceneName);
        }
        else
        {
            Debug.LogWarning("No save data found.");
        }
    }

    public void aboutMenu(){
        sfxManager.instance.sfx_1();
        mainMenuUI.SetActive(false);
        aboutMenuUI.SetActive(true);
        playButton.SetActive(false);
        aboutButton.SetActive(false);
        exitButton.SetActive(false);
        backButton.SetActive(true);
        settingButton.SetActive(false);
        settingMenuUI.SetActive(false);
    }

    public void quitGame(){
        sfxManager.instance.sfx_1();
        Application.Quit();
        Debug.Log("Quit");
    }

    public void backMenu(){
        sfxManager.instance.sfx_1();
        Panelmainmenu();
    }

    private void Panelmainmenu(){
        sfxManager.instance.sfx_1();
        mainMenuUI.SetActive(true);
        aboutMenuUI.SetActive(false);
        playButton.SetActive(true);
        aboutButton.SetActive(true);
        exitButton.SetActive(true);
        backButton.SetActive(false);
        settingButton.SetActive(true);
        settingMenuUI.SetActive(false);
    }

    public void settingMenu(){
        sfxManager.instance.sfx_1();
        mainMenuUI.SetActive(false);
        aboutMenuUI.SetActive(false);
        playButton.SetActive(false);
        aboutButton.SetActive(false);
        exitButton.SetActive(false);
        backButton.SetActive(true);
        settingButton.SetActive(false);
        settingMenuUI.SetActive(true);
    }
}
