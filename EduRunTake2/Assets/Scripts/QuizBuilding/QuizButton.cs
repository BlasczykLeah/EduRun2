﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuizButton : MonoBehaviour, IPointerClickHandler
{
    // if true:  chooses quiz to play game
    // if false: chooses quiz to edit
    public bool purposeSwap;
    public int quizIndex;
    GameManager gm;
    QuizBuilder qb;
    Button playBtn;

    public void OnPointerClick(PointerEventData eventData)
    {
        gm.chosenQuiz = quizIndex;
        FXplayer.fxplayer.PlayFX(fxOptions.tap);

        if (purposeSwap)
        {
            // playing game

            gm.ResetColorQB();
            GetComponent<Image>().color = new Color32(158, 255, 148, 255);

            if (playBtn == null) playBtn = GameObject.Find("GameplayMenu").transform.GetChild(0).GetComponent<Button>();
            playBtn.interactable = true;

            // this button does NOT start the game
        }
        else
        {
            // opening quiz editor

            qb.gameObject.SetActive(true);
            qb.openQuizBuilder(gm.quizStorage[quizIndex]);

            // this button DOES open the quiz editor
        }
    }

    void Start()
    {
        gm = GameManager.inst;
        qb = QuizBuilder.inst;
    }
}
