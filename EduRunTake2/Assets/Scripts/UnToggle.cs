using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UnToggle : MonoBehaviour, IPointerClickHandler
{
    public Toggle[] otherToggles;

    public void OnPointerClick(PointerEventData eventData)
    {
        foreach(Toggle t in otherToggles)
        {
            if (t.isOn) t.isOn = false;
        }
    }
}
