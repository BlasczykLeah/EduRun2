using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class NextQuestion : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        FXplayer.fxplayer.PlayFX(fxOptions.tap);
        SceneManager.LoadScene(1);
    }
}
