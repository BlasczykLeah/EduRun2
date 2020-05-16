using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    public List<QuizContainer> quizStorage; // load in quizzes here
    public List<Question> activeQuiz;
    public int chosenQuiz;

    public GameObject layoutGroup;
    public GameObject quizButtonPref;
    public List<QuizButton> quizButtons;

    List<Question> completedQuestions;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (inst == null) inst = this;
        else
        {
            // load quiz buttons on actual inst
            Destroy(this.gameObject);
        }

        //quizStorage = new List<QuizContainer>();
        //quizStorage = SaveLoad.LoadQuestionSet();
    }

    public void addQuizToSave(QuizContainer newQuiz)
    {
        if (quizStorage.Count > chosenQuiz && chosenQuiz != -1)
        {
            quizStorage[chosenQuiz] = newQuiz;
            quizButtons[chosenQuiz].transform.GetChild(0).GetComponent<TMP_Text>().text = quizStorage[chosenQuiz].container[quizStorage[chosenQuiz].container.Count - 1].question;
        }
        else
        {
            quizStorage.Add(newQuiz);
            AddQuizButton();
        }
        SaveLoad.SaveQuizSet(quizStorage);
    }

    public Question pullQuestion()
    {
        if (activeQuiz == null)
        {
            Debug.LogError("No active quiz");
            return null;
        }

        if(activeQuiz.Count > 1)    // the last question is just the name of the quiz; it cannot be picked
        {
            int randQuestion = UnityEngine.Random.Range(0, activeQuiz.Count - 1);
            Question quest = activeQuiz[randQuestion];
            activeQuiz.Remove(quest);

            completedQuestions.Add(quest);
            return quest;
        }
        else
        {
            Debug.Log("No questions left. This quiz should be over.");
            return null;
        }
    }

    public void setActiveQuiz(int index)
    {
        activeQuiz = new List<Question>();
        foreach(Question a in quizStorage[index].container)
        {
            string[] array = new string[3] { a.answers[0], a.answers[1], a.answers[2] };
            Question temp = new Question(a.question, array, a.correctIndex);
            activeQuiz.Add(temp);
        }
    }

    public void LoadQuizButtons()   // this is called every time the main menu opens
    {
        quizStorage = new List<QuizContainer>();
        quizStorage = SaveLoad.LoadQuestionSet();
        completedQuestions = new List<Question>();

        layoutGroup = GameObject.Find("QuizList");

        quizButtons = new List<QuizButton>();

        for(int i = 0; i < quizStorage.Count; i++)
        {
            AddQuizButton();
        }
        layoutGroup.transform.parent.gameObject.SetActive(false);
        // set main menu
    }

    // makes a new quiz button at the end of the list
    public void AddQuizButton()
    {
        GameObject newButton = Instantiate(quizButtonPref);
        int newIndex;
        newIndex = newButton.GetComponent<QuizButton>().quizIndex = quizButtons.Count;
        quizButtons.Add(newButton.GetComponent<QuizButton>());

        newButton.transform.GetChild(0).GetComponent<TMP_Text>().text = quizStorage[newIndex].container[quizStorage[newIndex].container.Count - 1].question;

        newButton.transform.SetParent(layoutGroup.transform, false); //might need to fix scale too
    }

    // removes quiz button at index
    public void RemoveQuizButton(int index)
    {
        quizButtons.RemoveAt(index);
        Destroy(layoutGroup.transform.GetChild(index).gameObject);

        for(int i = chosenQuiz; i < quizButtons.Count; i++)
        {
            quizButtons[i].quizIndex = i;
        }
    }

    public void ResetColorQB()
    {
        foreach(QuizButton a in quizButtons)
        {
            a.GetComponent<Image>().color = Color.white;
        }
    }

    public void finalQuestions(GameObject questionsHolder)
    {
        //fill questions
        if (completedQuestions.Count <= 0) return;

        for(int i = 0; i < completedQuestions.Count; i++)
        {
            questionsHolder.transform.GetChild(i).gameObject.SetActive(true);
            GameObject myBox = questionsHolder.transform.GetChild(i).gameObject;

            myBox.transform.GetChild(0).GetComponent<TMP_Text>().text = completedQuestions[i].question;
            myBox.transform.GetChild(1).GetComponent<TMP_Text>().text = completedQuestions[i].answers[0];
            myBox.transform.GetChild(2).GetComponent<TMP_Text>().text = completedQuestions[i].answers[1];
            myBox.transform.GetChild(3).GetComponent<TMP_Text>().text = completedQuestions[i].answers[2];

            myBox.transform.GetChild(completedQuestions[i].correctIndex + 1).GetComponent<TMP_Text>().color = new Color32(158, 255, 148, 255);
        }
    }
}
