using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisablePlay : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Button>().interactable = false;
    }
}
