using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManger : MonoBehaviour
{
    [Header("Main Menu UI")]
    public GameObject mainMenuUI;
    public GameObject aboutMenuUI;
    public GameObject playButton;
    public GameObject aboutButton;
    public GameObject exitButton;
    public GameObject backButton;

    // Start is called before the first frame update
    void Start()
    {
        mainMenuUI.SetActive(true);
        aboutMenuUI.SetActive(false);
        playButton.SetActive(true);
        aboutButton.SetActive(true);
        exitButton.SetActive(true);
        backButton.SetActive(false);
    }

    public void loadScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }

    public void aboutMenu(){
        mainMenuUI.SetActive(false);
        aboutMenuUI.SetActive(true);
        playButton.SetActive(false);
        aboutButton.SetActive(false);
        exitButton.SetActive(false);
        backButton.SetActive(true);
    }

    public void quitGame(){
        Application.Quit();
        Debug.Log("Quit");
    }

    public void backMenu(){
        Start();
    }
}
