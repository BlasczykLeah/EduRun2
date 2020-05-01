using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizBuilder : MonoBehaviour
{
    [Header("Question Inputs")]
    public TMP_InputField questionTxt;
    public TMP_InputField a1Txt;
    public TMP_InputField a2Txt;
    public TMP_InputField a3Txt;
    public Toggle[] toggles;

    [Header("Other GameObjects")]
    public GameObject buttonLayoutGroup;
    public GameObject questionButton;

    // need to reset this whenever a new quiz is created
    public List<Question_NH> unfinishedQuiz;

    // Start is called before the first frame update
    void Start()
    {
        unfinishedQuiz = new List<Question_NH>();
    }

    public void saveQuestion(int index)
    {
        int correct = -1;
        for(int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].isOn)
            {
                correct = i;
                toggles[i].isOn = false;
            }
        }

        // check for correctness:
        if(questionTxt.text == "" || a1Txt.text == "" || a2Txt.text == "" || a3Txt.text == "")
        {
            Debug.Log("Invalid text inputs");
            return;
        }
        else if(correct == -1)
        {
            Debug.Log("Choose correct answer");
            return;
        }

        // gather all question components
        string[] answers = new string[3] { a1Txt.text, a2Txt.text, a3Txt.text };
        Question_NH newQuestion = new Question_NH(questionTxt.text, answers, correct);

        if (unfinishedQuiz.Count > index) unfinishedQuiz[index] = newQuestion;
        else unfinishedQuiz.Add(newQuestion);

        resetInputs();

        questionTxt.transform.parent.gameObject.SetActive(false);

        //List<Question_NH> qSet = SaveLoad.LoadQuestionSet();
        //qSet.Add(newQuestion);


        //SaveLoad.SaveQuestionSet(qSet);

        //Debug.Log(currentQuestion + " created and saved.");
    }

    public void saveQuiz()
    {
        if (unfinishedQuiz.Count > 0)
            SaveLoad.SaveQuestionSet(unfinishedQuiz);
        else Debug.Log("Input more questions!");
    }

    public void deleteQuestion(int index)
    {
        if (unfinishedQuiz.Count > index) {
            for (int i = index + 1; i < unfinishedQuiz.Count; i++)
            {
                //find button, update tmp, update myIndex
                GameObject temp = buttonLayoutGroup.transform.GetChild(i).gameObject;
                temp.transform.GetChild(0).GetComponent<TMP_Text>().text = (i).ToString();
                temp.GetComponent<QuestionButton>().myIndex--;
            }

            unfinishedQuiz.RemoveAt(index);
        }
        Destroy(buttonLayoutGroup.transform.GetChild(index).gameObject);

        resetInputs();
        questionTxt.transform.parent.gameObject.SetActive(false);
    }

    public void newQuestionButton()
    {
        // max question count here:
        if (unfinishedQuiz.Count >= 15) return;

        GameObject button = Instantiate(questionButton);

        button.transform.SetParent(buttonLayoutGroup.transform);
        button.transform.SetSiblingIndex(button.transform.GetSiblingIndex() - 1);
        button.transform.GetChild(0).GetComponent<TMP_Text>().text = (unfinishedQuiz.Count + 1).ToString() ;
        
        button.transform.localScale = new Vector3(1, 1, 1);
        button.transform.localPosition = Vector3.zero;

        QuestionButton temp = button.AddComponent<QuestionButton>();
        temp.constructor(unfinishedQuiz.Count, this, questionTxt.transform.parent.gameObject);
    }

    void resetInputs()
    {
        questionTxt.text = a1Txt.text = a2Txt.text = a3Txt.text = "";
        foreach (Toggle t in toggles) t.isOn = false;
    }







    public void loadQuestionOld(int index)
    {
        // goind to change how this works, leaving original code here

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
