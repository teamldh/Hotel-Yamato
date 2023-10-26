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
    public GameObject playButton;
    public GameObject aboutButton;
    public GameObject exitButton;
    public GameObject backButton;
    public Button continueButton;
    public TextMeshProUGUI guestName1;
    public TextMeshProUGUI guestName2;

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void continueGame(){
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
        Panelmainmenu();
    }

    private void Panelmainmenu(){
        mainMenuUI.SetActive(true);
        aboutMenuUI.SetActive(false);
        playButton.SetActive(true);
        aboutButton.SetActive(true);
        exitButton.SetActive(true);
        backButton.SetActive(false);
    }
}
