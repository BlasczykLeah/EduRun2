using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuizButton : MonoBehaviour, IPointerClickHandler
{
    // if true:  chooses quiz to play game
    // if false: chooses quiz to edit
    public bool purposeSwap;
    public int quizIndex;
    GameManager gm;
    QuizBuilder qb;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (purposeSwap)
        {
            // playing game
            gm.chosenQuiz = quizIndex;
            // other game setup stuffs
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

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.inst;
        qb = QuizBuilder.inst;
    }
}
