using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeleteButton : MonoBehaviour, IPointerClickHandler
{
    public QuizBuilder inst;
    public int activeQuestion;

    public void OnPointerClick(PointerEventData eventData)
    {
        inst.deleteQuestion(activeQuestion);
    }
}