using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizScoreManager : MonoBehaviour
{
    private static QuizScoreManager _instance;
    public static QuizScoreManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<QuizScoreManager>();
                
            }
            return _instance;
        }
    }

    public int PlayerScore { get; private set; }
    public List<bool> PlayerAnswers { get; private set; }
    public List<string> question { get; private set;}

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        PlayerAnswers = new List<bool>();
        question = new List<string>();
    }

    public void IncreaseScore(bool isCorrect)
    {
        if (isCorrect)
        {
            PlayerScore++;
        }

        PlayerAnswers.Add(isCorrect);
    }

    public void addQuestion(string question)
    {
        this.question.Add(question);
    }

    public void ResetScore()
    {
        PlayerScore = 0;
        PlayerAnswers.Clear();
        question.Clear();
    }
}
