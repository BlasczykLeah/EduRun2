using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class QuestionButton : MonoBehaviour, IPointerClickHandler
{
    public int myIndex;

    bool isNew;
    GameObject questionInput;   // child indexes: Backround = 0, Question = 1, Answer1 = 2, Answer2 = 3, Answer3 = 4, Toggle1 = 5, Toggle2 = 6, Toggle3 = 7, Save = 8, Delete = 9
    SaveButton saveBtn;
    DeleteButton dltBtn;
    QuizBuilder inst;   // might make this a singleton

    public void constructor(int num, QuizBuilder qb, GameObject qi)
    {
        myIndex = num;
        inst = qb;
        questionInput = qi;
        saveBtn = questionInput.transform.GetChild(8).GetComponent<SaveButton>();
        dltBtn = questionInput.transform.GetChild(9).GetComponent<DeleteButton>();
        isNew = true;

        OnPointerClick(null);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // open QuestionInput and sending myIndex to Save button
        // if this question has been saved before, load question/answers/toggles correctly

        FXplayer.fxplayer.PlayFX(fxOptions.tap);

        saveBtn.activeQuestion = myIndex;
        dltBtn.activeQuestion = myIndex;
        questionInput.SetActive(true);

        if (!isNew)
        {
            questionInput.transform.GetChild(1).GetComponent<TMPro.TMP_InputField>().text = inst.unfinishedQuiz[myIndex].question;

            questionInput.transform.GetChild(2).GetComponent<TMPro.TMP_InputField>().text = inst.unfinishedQuiz[myIndex].answers[0];
            questionInput.transform.GetChild(3).GetComponent<TMPro.TMP_InputField>().text = inst.unfinishedQuiz[myIndex].answers[1];
            questionInput.transform.GetChild(4).GetComponent<TMPro.TMP_InputField>().text = inst.unfinishedQuiz[myIndex].answers[2];

            questionInput.transform.GetChild(5 + inst.unfinishedQuiz[myIndex].correctIndex).GetComponent<Toggle>().isOn = true;
        }
        else
        {
            isNew = false;
        }
    }
}
