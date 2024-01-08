using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class QuizHandler : MonoBehaviour
{
    public Quiz quiz;
    public TextMeshProUGUI soal;
    public TextMeshProUGUI[] jawaban;
    public GameObject panelHasil;
    public GameObject panelSoal;

    int nomorSoal;
    // // Start is called before the first frame update
    // void Start()
    // {
    //     generateSoal();
    // }

    void OnEnable()
    {
        generateSoal();
        panelSoal.SetActive(true);
    }

    private void generateSoal(){
        soal.text = quiz.elementsSoal[nomorSoal].elementsPertanyaan.soal;

        for(int i = 0; i < jawaban.Length; i++){
            jawaban[i].text = quiz.elementsSoal[nomorSoal].elementsPertanyaan.jawaban[i];
        }
    }

    public void buttonJawaban(){
        TextMeshProUGUI jawabanBenar = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        bool isCorrect = jawabanBenar.text == quiz.elementsSoal[nomorSoal].elementsPertanyaan.jawaban[quiz.elementsSoal[nomorSoal].elementsPertanyaan.jawabanBenar];
        string pertanyaan = quiz.elementsSoal[nomorSoal].elementsPertanyaan.soal;

        if(isCorrect){
            Debug.Log("Jawaban Benar");
            QuizScoreManager.Instance.IncreaseScore(true);
        }else{
            Debug.Log("Jawaban Salah");
            QuizScoreManager.Instance.IncreaseScore(true);
        }

        QuizScoreManager.Instance.addQuestion(pertanyaan);
        
        panelHasil.SetActive(true);
        panelHasil.transform.GetChild(0).gameObject.SetActive(isCorrect);
        panelHasil.transform.GetChild(1).gameObject.SetActive(!isCorrect);
    }

    public void nextScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
