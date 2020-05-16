using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

[System.Serializable]
public class QuizContainer
{
    public List<Question> container;

    public QuizContainer(List<Question> myList)
    {
        container = myList;
    }
}

public class QuizBuilder : MonoBehaviour
{
    public static QuizBuilder inst;
    GameManager gm;

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
    public GameObject confirmDeleteBox;
    public GameObject errorBox;

    public GameObject quizzesMenu;
    public GameObject gameplayMenu;
    public GameObject quizEditorMenu;
    public GameObject mainMenu;
    QuizContainer editedQuiz;

    // need to reset this whenever a new quiz is created
    public List<Question> unfinishedQuiz;

    private void Awake()
    {
        if (inst == null)
        {
            inst = this;
            gm = GameManager.inst;
            gm.LoadQuizButtons();
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }
        else Destroy(this);
    }

    public void saveQuestion(int index)
    {
        int correct = -1;
        for (int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].isOn)
            {
                correct = i;
                toggles[i].isOn = false;
            }
        }

        // check for correctness:
        if (questionTxt.text == "" || a1Txt.text == "" || a2Txt.text == "" || a3Txt.text == "")
        {
            //Debug.Log("Invalid text inputs");
            showErrorBox("Invalid text inputs.");
            return;
        }
        else if (correct == -1)
        {
            //Debug.Log("Choose correct answer");
            showErrorBox("Choose a correct answer.");
            return;
        }

        // gather all question components
        string[] answers = new string[3] { a1Txt.text, a2Txt.text, a3Txt.text };
        Question newQuestion = new Question(questionTxt.text, answers, correct);

        if (unfinishedQuiz.Count > index) unfinishedQuiz[index] = newQuestion;
        else
        {
            unfinishedQuiz.Add(newQuestion);
            Debug.Log("Question added");
        }

        resetInputs();

        questionTxt.transform.parent.gameObject.SetActive(false);
    }

    public void saveQuiz()
    {
        if (unfinishedQuiz.Count > 0)
        {
            string[] none = new string[3] { "", "", "" };
            if (quizTitle.text == "")
            {
                //Debug.Log("Invalid quiz name");
                showErrorBox("Enter a quiz name.");
                return;
            }
            Question title = new Question(quizTitle.text, none, -1);    // adding one more Question that holds the name of the quiz, this is not an actual question
            unfinishedQuiz.Add(title);

            //SaveLoad.SaveQuizSet(unfinishedQuiz);
            QuizContainer newQuiz = new QuizContainer(unfinishedQuiz);
            gm.addQuizToSave(newQuiz);

            resetMenus();
            backToMain();
        }
        else
        {
            //Debug.Log("Input more questions!");
            showErrorBox("Input more questions!");
        }
    }


    void showErrorBox(string words)
    {
        errorBox.SetActive(true);
        errorBox.transform.GetChild(0).GetComponent<TMP_Text>().text = words;
        Invoke("hideErrorBox", 2.2F);
    }
    void hideErrorBox()
    {
        errorBox.transform.GetChild(0).GetComponent<TMP_Text>().text = "";
        errorBox.SetActive(false);
    }

    void resetMenus()
    {
        //reset stuff
        quizTitle.text = "";
        foreach (QuestionButton a in buttonLayoutGroup.transform.GetComponentsInChildren<QuestionButton>()) Destroy(a.gameObject);
        unfinishedQuiz = new List<Question>();
        confirmDeleteBox.SetActive(false);
        questionTxt.transform.parent.gameObject.SetActive(false);
        buttonLayoutGroup.transform.parent.gameObject.SetActive(false);
        editedQuiz = null;
    }

    public void openQuizBuilder(QuizContainer quiz)
    {
        gameObject.SetActive(true);
        quizzesMenu.SetActive(false);
        quizEditorMenu.SetActive(false);

        editedQuiz = quiz;
        unfinishedQuiz = new List<Question>();
        for(int i = 0; i < quiz.container.Count - 1; i++)
        {
            newQuestionButton();
            questionTxt.text = quiz.container[i].question;
            a1Txt.text = quiz.container[i].answers[0];
            a2Txt.text = quiz.container[i].answers[1];
            a3Txt.text = quiz.container[i].answers[2];
            toggles[quiz.container[i].correctIndex].isOn = true;

            saveQuestion(i);
        }
        quizTitle.text = quiz.container[unfinishedQuiz.Count].question;
    }

    public void showConfirmDelete()
    {
        confirmDeleteBox.SetActive(true);
    }
    public void confirmDeleteQuiz(bool delete)
    {
        if (delete)
        {
            deleteQuiz();
            resetMenus();
            backToMain();
        }
        else
        {
            // disable this window
            confirmDeleteBox.SetActive(false);
        }
    }

    void deleteQuiz()
    {
        if(editedQuiz != null)
        {
            int index = gm.quizStorage.IndexOf(editedQuiz);
            gm.RemoveQuizButton(index);

            gm.quizStorage.Remove(editedQuiz);
            SaveLoad.SaveQuizSet(gm.quizStorage);
            editedQuiz = null;
            return;
        }// else it should be removed by simply resetting everything, already done
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

    public void startNewQuiz()
    {
        gameObject.SetActive(true);
        quizzesMenu.SetActive(false);
        quizEditorMenu.SetActive(false);
        unfinishedQuiz = new List<Question>();

        gm.chosenQuiz = -1;
    }

    void resetInputs()
    {
        questionTxt.text = a1Txt.text = a2Txt.text = a3Txt.text = "";
        foreach (Toggle t in toggles) t.isOn = false;
    }

    public void openQuizEditingMenu()
    {
        quizEditorMenu.SetActive(true);
        quizzesMenu.SetActive(true);
        foreach (QuizButton a in gm.quizButtons) a.purposeSwap = false;
        mainMenu.SetActive(false);
    }

    public void openGameplayMenu()
    {
        gameplayMenu.SetActive(true);
        quizzesMenu.SetActive(true);
        foreach (QuizButton a in gm.quizButtons) a.purposeSwap = true;
        gameplayMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void backToMain()
    {
        gm.chosenQuiz = -1;
        gm.ResetColorQB();

        quizzesMenu.SetActive(false);
        gameplayMenu.SetActive(false);
        quizEditorMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void startGame() //menu = 0, game = 1
    {
        gm.setActiveQuiz(gm.chosenQuiz);
        SceneManager.LoadScene(1);
    }
}
