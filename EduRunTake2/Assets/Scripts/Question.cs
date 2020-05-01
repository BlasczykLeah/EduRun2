using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question
{
    public string question;
    public string[] answers;
    public int correctIndex;

    public Question(string q, string[] a, int i)
    {
        question = q;
        answers = a;
        correctIndex = i;
    }
}
