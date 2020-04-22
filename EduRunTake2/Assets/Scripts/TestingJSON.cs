using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingJSON : MonoBehaviour
{
    private string QUESTION_1 = "Question1";

    // Start is called before the first frame update
    void Start()
    {
        /*
        if (!PlayerPrefs.HasKey(QUESTION_1))
        {
            //Question q1 = new Question();
            //q1.question = "how are you?";
            //q1.answers = new string[3] { "gud", "eh", "AMAZING" };
            //q1.correctIndex = 2;

            string qJSON = JsonUtility.ToJson(q1);
            PlayerPrefs.SetString(QUESTION_1, qJSON);

            Debug.Log("Question Created");
        }
        else
        {
            string qJSON = PlayerPrefs.GetString(QUESTION_1);
            Question q1 = JsonUtility.FromJson<Question>(qJSON);

            Debug.Log(q1.question);
            Debug.Log("The correct answer is " + q1.answers[q1.correctIndex]);
        }*/
    }
}
