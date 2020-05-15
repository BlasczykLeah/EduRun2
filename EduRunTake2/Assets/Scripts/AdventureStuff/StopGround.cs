using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopGround : MonoBehaviour
{
    public Animator playerAnim;

    public void StopAnim()
    {
        playerAnim.SetBool("Stopped", true);
    }
}
