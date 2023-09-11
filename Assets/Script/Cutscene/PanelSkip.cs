using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelSkip : MonoBehaviour
{
    [SerializeField]private string sceneName = "";
    [SerializeField]private int sceneIndex = 0;
    
    public GameObject panelpause;
    public GameObject panelSkip;
    public GameObject panelMainMenu;

    private void Start() {
        panelpause.SetActive(false);
        panelSkip.SetActive(false);
        panelMainMenu.SetActive(false);
    }
    private void Update() {
        sceneName = SceneManager.GetActiveScene().name;
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(Input.GetKeyDown(KeyCode.Escape)){
            Pause();
        }
    }
    public void Pause(){
        panelpause.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume(){
        Start();
        Time.timeScale = 1f;
    }

    public void Skip(){
        panelSkip.SetActive(true);
    }

    public void MainMenu(){
        panelMainMenu.SetActive(true);
    }

    public void Yes(string name){
        SceneManager.LoadScene(name);
        Time.timeScale = 1f;
    }

    public void No(){
        panelSkip.SetActive(false);
    }

    public void YesMainMenu(string name){
        GameData data = new GameData(sceneName, sceneIndex);
        SaveManager.instance.SaveSceneData(data);
        SceneManager.LoadScene(name);
        Time.timeScale = 1f;
    }

    public void NoMainMenu(){
        panelMainMenu.SetActive(false);
    }


}
