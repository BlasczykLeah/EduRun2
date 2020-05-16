using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour, IPointerClickHandler
{
    public float waitTime = 2F;
    public bool iAmRight = false;

    public GameObject player;

    public GameQuestion questionBox;

    public void OnPointerClick(PointerEventData eventData)
    {
        questionBox.disableButtons();

        if (iAmRight)
        {
            // good things happen
            GetComponent<Image>().color = Color.green;
            questionBox.GetComponent<GameQuestion>().correctAnswer();
        }
        else
        {
            // bad things happen
            GetComponent<Image>().color = Color.red;

            player.GetComponent<PlayerHealth>().TakeDamage();

            StartCoroutine(backToBoss(waitTime));
        }
    }

    IEnumerator backToBoss(float time)
    {
        yield return new WaitForSecondsRealtime(time);

        Time.timeScale = 1;
        questionBox.gameObject.SetActive(false);
        questionBox.answerBtns.Remove(this);
        GetComponent<Image>().color = Color.grey;

        questionBox.GetComponent<GameQuestion>().boss.GetComponent<BossMove>().moveToStart();

        Destroy(this);
    }
}
