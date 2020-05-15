using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour, IPointerClickHandler
{
    public float waitTime = 2F;
    public bool iAmRight = false;

    public GameQuestion thing;

    public void OnPointerClick(PointerEventData eventData)
    {
        thing.disableButtons();

        if (iAmRight)
        {
            // good things happen
            GetComponent<Image>().color = Color.green;
        }
        else
        {
            // bad things happen
            GetComponent<Image>().color = Color.red;
            StartCoroutine(backToBoss(waitTime));
        }
    }

    IEnumerator backToBoss(float time)
    {
        yield return new WaitForSecondsRealtime(time);

        thing.gameObject.SetActive(false);
        thing.answerBtns.Remove(this);
        GetComponent<Image>().color = Color.grey;
        Destroy(this);
    }
}
