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

    public GameObject nextBtn, quitBtn; //can add show score btn later
    bool gameOver = false;
    
    public GameObject boss;

    Question roundQuestion;
    GameManager gm;

    void Start()
    {
        Time.timeScale = 1;

        if(GameManager.inst == null)
        {
            Debug.LogError("GameManager not found");
            Destroy(gameObject);
        }

        gm = GameManager.inst;          // check if out of questions when round ends
        roundQuestion = gm.pullQuestion();

        questionTxt.text = roundQuestion.question;

        int[] rand = randNums();
        for(int i = 0; i < answerBtns.Count; i++)
        {
            answerBtns[rand[i]].transform.GetChild(0).GetComponent<TMP_Text>().text = roundQuestion.answers[i];
            answerBtns[rand[i]].questionBox = this;

            if(i == roundQuestion.correctIndex) answerBtns[rand[i]].iAmRight = true;
        }
        //answerBtns[roundQuestion.correctIndex].iAmRight = true;

        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        for (int i = 0; i < answerBtns.Count; i++)
        {
            answerBtns[i].GetComponent<Image>().raycastTarget = true;
            answerBtns[i].interactable = true;
        }
    }

    public void disableButtons()
    {
        for (int i = 0; i < answerBtns.Count; i++)
        {
            answerBtns[i].GetComponent<Image>().raycastTarget = false;
            answerBtns[i].interactable = false;
        }
    }

    public void correctAnswer()
    {
        // check if next question or quiz ended, invoke correct method
        foreach (AnswerButton a in answerBtns) a.enabled = false;

        if(gm.activeQuiz.Count <= 1)
        {
            // out of questions, show menu btn
            gameOver = true;
        }
        StartCoroutine(showBtn(1));
    }

    IEnumerator showBtn(float time)
    {
        yield return new WaitForSecondsRealtime(time);

        if (gameOver) quitBtn.SetActive(true);
        else nextBtn.SetActive(true);
    }

    public void nextQuestion()
    {
        SceneManager.LoadScene(1);
    }

    public void backToMenu()
    {
        SceneManager.LoadScene(0);
    }

    int[] randNums()
    {
        int[] nums = new int[3];

        nums[0] = nums[1] = nums[2] = Random.Range(0, 3);
        while (nums[1] == nums[0]) nums[1] = Random.Range(0, 3);
        while (nums[2] == nums[1] || nums[2] == nums[0]) nums[2] = Random.Range(0, 3);

        return nums;
    }
}
