using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class EndScript : MonoBehaviour, IPointerClickHandler
{
    public GameObject thing;

    public void OnPointerClick(PointerEventData eventData)
    {
        FXplayer.fxplayer.PlayFX(fxOptions.tap);
        SceneManager.LoadScene(0);
    }

    private void OnEnable()
    {
        GameManager.inst.finalQuestions(thing);
    }
}
