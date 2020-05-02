using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
//using UnityEngine.UIElements;

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
    public TMP_InputField quizTitle;

    [Header("Testing GameObjects")]
    public GameObject tempProof;
    public TMP_Text titleTemp;
    public TMP_Text questionTemp;

    // need to reset this whenever a new quiz is created
    public List<Question> unfinishedQuiz;

    // Start is called before the first frame update
    void Start()
    {
        unfinishedQuiz = new List<Question>();
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
        Question newQuestion = new Question(questionTxt.text, answers, correct);

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
        {
            string[] none = new string[3] { "", "", "" };
            if(quizTitle.text == "")
            {
                Debug.Log("Invalid quiz name");
                return;
            }
            Question title = new Question(quizTitle.text, none, -1);    // adding one more Question that holds the name of the quiz, this is not an actual question
            unfinishedQuiz.Add(title);

            SaveLoad.SaveQuestionSet(unfinishedQuiz);

            //reset stuff
            quizTitle.text = "";
            foreach (QuestionButton a in buttonLayoutGroup.transform.GetComponentsInChildren<QuestionButton>()) Destroy(a.gameObject);
            unfinishedQuiz = new List<Question>();
            buttonLayoutGroup.transform.parent.gameObject.SetActive(false);
            testingLoadQuiz2();
        }
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





    public void testingLoadQuiz()
    {
        List<Question> quiz = SaveLoad.LoadQuestionSet();
        Debug.Log(quiz[quiz.Count - 1].question);

        for(int i = 0; i < quiz.Count - 1; i++)
        {
            Debug.Log("");
            Debug.Log(quiz[i].question);
            Debug.Log(quiz[i].answers[0]);
            Debug.Log(quiz[i].answers[1]);
            Debug.Log(quiz[i].answers[2]);
            Debug.Log("The correct answer is: " + quiz[i].answers[quiz[i].correctIndex]);
        }
    }

    public void testingLoadQuiz2()
    {
        tempProof.SetActive(true);

        List<Question> quiz = SaveLoad.LoadQuestionSet();
        titleTemp.text = quiz[quiz.Count - 1].question;

        string finalOutput = "%";
        for (int i = 0; i < quiz.Count - 1; i++)
        {
            finalOutput += ("%" + quiz[i].question);
            finalOutput += ("%1. " + quiz[i].answers[0]);
            finalOutput += ("%2. " + quiz[i].answers[1]);
            finalOutput += ("%3. " + quiz[i].answers[2]);
            finalOutput += ("%The correct answer is: " + quiz[i].answers[quiz[i].correctIndex]);
        }

        string thing = finalOutput.Replace("%", System.Environment.NewLine);
        questionTemp.text = thing;
    }

    public void loadQuestionOld(int index)
    {
        // goind to change how this works, leaving original code here

        List <Question> qSet = SaveLoad.LoadQuestionSet();
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
