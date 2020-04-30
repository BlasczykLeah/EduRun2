using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question_NH
{
    public string question;
    public string[] answers;
    public int correctIndex;

    public Question_NH(string q, string[] a, int i)
    {
        question = q;
        answers = a;
        correctIndex = i;
    }
}

