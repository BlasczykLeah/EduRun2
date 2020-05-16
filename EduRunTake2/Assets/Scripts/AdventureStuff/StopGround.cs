using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopGround : MonoBehaviour
{
    public Animator playerAnim;
    public Animator swordAnim;
    public GameObject Boss;

    public void StopAnim()
    {
        playerAnim.SetBool("Stopped", true);
        swordAnim.SetBool("Stop", true);
        Boss.SetActive(true);
    }
}
