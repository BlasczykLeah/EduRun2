using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quiz
{
    public int questionCount;
    public List<Question> questions;
    public string quizName;

    public Quiz(int count)
    {
        questionCount = count;
        questions = new List<Question>();

        if (PlayerPrefs.HasKey("QuizCount"))
        {
            quizName = "Quiz" + PlayerPrefs.GetInt("QuizCount");
            PlayerPrefs.SetInt("QuizCount", PlayerPrefs.GetInt("QuizCount") + 1);
        }
        else
        {
            quizName = "Quiz" + 1;
            PlayerPrefs.SetInt("QuizCount", 1);
        }
    }
}
