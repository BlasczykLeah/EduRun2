using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TestingInputs : MonoBehaviour
{
    public TMP_InputField questionTxt;
    public TMP_InputField a1Txt;
    public TMP_InputField a2Txt;
    public TMP_InputField a3Txt;
    public Toggle[] toggles;



    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("QuestionCount"))
        {
            PlayerPrefs.SetInt("QuestionCount", 1);
        }
    }

    public void saveQuestion()
    {
        // make sure all fields are filled and one correct answer
        // make all things uninteractible

        int correct = -1;
        for(int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].isOn) correct = i;
        }

        string[] answers = new string[3] { a1Txt.text, a2Txt.text, a3Txt.text };
        Question q = new Question(questionTxt.text, answers, correct);

        string currentQuestion = "Question" + PlayerPrefs.GetInt("QuestionCount");     //Question1
        PlayerPrefs.SetInt("QuestionCount", PlayerPrefs.GetInt("QuestionCount") + 1);
        string qJSON = JsonUtility.ToJson(q);
        PlayerPrefs.SetString(currentQuestion, qJSON);

        Debug.Log(currentQuestion + " created and saved.");
    }

    public void loadQuestion(int index)
    {
        string currentQuestion = "Question" + index;
        if (PlayerPrefs.HasKey(currentQuestion))
        {
            string qJSON = PlayerPrefs.GetString(currentQuestion);
            Question q = JsonUtility.FromJson<Question>(qJSON);

            Debug.Log(q.question);
            foreach (string a in q.answers) Debug.Log(a);
            Debug.Log("The correct answer is " + q.answers[q.correctIndex]);
        }
        else
        {
            Debug.Log("Question not found.");
        }
    }
}
