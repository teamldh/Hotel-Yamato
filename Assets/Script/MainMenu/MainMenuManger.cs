using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuManger : MonoBehaviour
{
    public login Login;
    [Header("Main Menu UI")]
    public GameObject mainMenuUI;
    public GameObject aboutMenuUI;
    public GameObject LoginPanel;
    public GameObject GuestPanel;
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
        string guest = Login.generateGuest();
        LoginPanel.SetActive(true);
        GuestPanel.SetActive(false);
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

        Login.loginButton.onClick.AddListener(Login.Login);
        Login.guest.onClick.AddListener(() => {
            LoginPanel.SetActive(false);
            GuestPanel.SetActive(true);
            guestName1.text = "hello " + guest;
            guestName2.text = "hello " + guest;
        });
        
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
        LoginPanel.SetActive(false);
        GuestPanel.SetActive(false);
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
        LoginPanel.SetActive(false);
        GuestPanel.SetActive(false);
        Panelmainmenu();
    }

    public void Yes(){
        LoginPanel.SetActive(false);
        GuestPanel.SetActive(false);
        Panelmainmenu();
    }

    public void No(){
        LoginPanel.SetActive(true);
        GuestPanel.SetActive(false);
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
