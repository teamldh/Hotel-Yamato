using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class QuizScoreUI : MonoBehaviour
{
    public TextMeshProUGUI skorText;
    public TextMeshProUGUI seluruhSoalText;
    public TextMeshProUGUI timerText;

    // Start is called before the first frame update
    void Start()
    {
        TampilkanSkor();
        TampilkanSeluruhSoal();
        GetTimer();
    }

    private void TampilkanSkor()
    {
        int skorPemain = QuizScoreManager.Instance.PlayerScore;
        int hitung = skorPemain/5*100;
        skorText.text = " " + hitung;
    }

    private void TampilkanSeluruhSoal()
    {
        for (int i = 0; i < QuizScoreManager.Instance.question.Count; i++)
        {
            string jawaban = QuizScoreManager.Instance.PlayerAnswers[i].ToString();
            if(jawaban == "True")
            {
                jawaban = "Benar";
            }
            else
            {
                jawaban = "Salah";
            }
            seluruhSoalText.text += QuizScoreManager.Instance.question[i] + "\n" + jawaban + "\n\n";
        }
    }

    private void GetTimer()
    {
        float timeS = TimerManager.instance.getTimerCount() % 60f;
        int timeM = Mathf.FloorToInt(TimerManager.instance.getTimerCount() / 60f);

        timerText.text = timeM.ToString("00") + ":" + timeS.ToString("00");
        //Debug.Log(timerCount);
    }

    public void sceneMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
