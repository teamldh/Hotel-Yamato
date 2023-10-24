using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOver : MonoBehaviour
{
    public GameObject PanelGameOver;
    // Start is called before the first frame update
    void Start()
    {
        PanelGameOver.SetActive(false);
    }

    public void gameOverPanel(){
        PanelGameOver.SetActive(true);
        Time.timeScale = 0f;
    }

    public void restartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void mainMenu(){
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }
}
