using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    // Start is called before the first frame update
    void Start()
    {
        pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerControllerInputSystem.GetInstance().GetPauseInput()){
            if(!pausePanel.activeSelf){
                pausePanel.SetActive(true);
                Time.timeScale = 0f;
            }
            else{
                pausePanel.SetActive(false);
                Time.timeScale = 1f;
            }
        }

        if(PlayerControllerInputSystem.GetInstance().GetResumeInput()){
            if(pausePanel.activeSelf){
                pausePanel.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }

    public void Resume(){
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void MainMenu(){
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
