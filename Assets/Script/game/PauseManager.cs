using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField]private string sceneName = "";
    [SerializeField]private int sceneIndex = 0;
    [SerializeField]private GameObject pausePanel;
    [SerializeField]private GameObject settingPanel;
    // Start is called before the first frame update
    void Start()
    {
        pausePanel.SetActive(false);
        settingPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        sceneName = SceneManager.GetActiveScene().name;
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(PlayerControllerInputSystem.GetInstance().GetPauseInput()){
            if(!pausePanel.activeSelf){
                sfxManager.instance.sfx_2();
                pausePanel.SetActive(true);
                Time.timeScale = 0f;
            }
            else{
                Resume();
            }
        }

        // if(PlayerControllerInputSystem.GetInstance().GetResumeInput()){
        //     if(pausePanel.activeSelf){
        //         Resume();
        //     }
        // }
    }

    public void Resume(){
        sfxManager.instance.sfx_2();
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void MainMenu(){
        sfxManager.instance.sfx_3();
        GameData data = new GameData(sceneName, sceneIndex);
        SaveManager.instance.SaveSceneData(data);
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void SettingPanel(){
        sfxManager.instance.sfx_1();
        settingPanel.SetActive(true);
    }

    public void SettingPanelClose(){
        sfxManager.instance.sfx_1();
        settingPanel.SetActive(false);
    }
}
