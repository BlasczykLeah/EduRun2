using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TestingInputs_NH : MonoBehaviour
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
        // gather all question components
        string[] answers = new string[3] { a1Txt.text, a2Txt.text, a3Txt.text };
        Question_NH newQuestion = new Question_NH(questionTxt.text, answers, correct);

        List<Question_NH> qSet = SaveLoad.LoadQuestionSet();
        qSet.Add(newQuestion);


        SaveLoad.SaveQuestionSet(qSet);

        //Debug.Log(currentQuestion + " created and saved.");
    }

    public void loadQuestion(int index)
    {
        List < Question_NH > qSet = SaveLoad.LoadQuestionSet();
        if (qSet.Count >= index )
        {

            Debug.Log(qSet[index].question);
            foreach (string a in qSet[index].answers) Debug.Log(a);
            Debug.Log("The correct answer is " + qSet[index].correctIndex);
        }
        else
        {
            Debug.Log("Question not found.");
        }
    }
}
