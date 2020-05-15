using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameQuestion : MonoBehaviour
{
    public List<AnswerButton> answerBtns;
    public TMP_Text questionTxt;

    public GameObject boss;

    Question roundQuestion;
    GameManager gm;

    void Start()
    {
        if(GameManager.inst == null)
        {
            Debug.LogError("GameManager not found");
            Destroy(gameObject);
        }

        gm = GameManager.inst;          // check if out of questions when round ends
        roundQuestion = gm.pullQuestion();

        questionTxt.text = roundQuestion.question;
        for(int i = 0; i < answerBtns.Count; i++)
        {
            answerBtns[i].transform.GetChild(0).GetComponent<TMP_Text>().text = roundQuestion.answers[i];
            answerBtns[i].questionBox = this;
        }
        answerBtns[roundQuestion.correctIndex].iAmRight = true;

        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        for (int i = 0; i < answerBtns.Count; i++)
        {
            answerBtns[i].GetComponent<Image>().raycastTarget = true;
        }
    }

    public void disableButtons()
    {
        for (int i = 0; i < answerBtns.Count; i++)
        {
            answerBtns[i].GetComponent<Image>().raycastTarget = false;
        }
    }

    public void correctAnswer()
    {
        // check if next question or quiz ended, invoke correct method
    }

    public void nextQuestion()
    {

    }

    public void backToMenu()
    {

    }
}
