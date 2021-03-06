﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int totalHealth;
    public int currHealth;
    public bool isDead;
    public GameObject deathScreen, boss;
    public GameObject disappointmentTxt;

    public GameObject healthBar;
    public Image[] hearts;
    public Sprite fullHeart, emptyHeart;

    void Start()
    {
        currHealth = totalHealth;
        isDead = false;
    }

    private void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < currHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
            gameObject.GetComponent<MeleeAttack>().count++;
            collision.gameObject.GetComponent<BasicEnemyHealth>().TakeDamage(999);
            if (GetComponent<MeleeAttack>().count < 3)
            {
                collision.gameObject.GetComponent<BasicEnemyHealth>().Spawn();
            }
        }
    }

    public void TakeDamage()
    {
        currHealth--;
        //Handheld.Vibrate();
        FXplayer.fxplayer.PlayFX(fxOptions.gethit);

        if (currHealth <= 0)
        {
            Death();
            Destroy(gameObject);
            Destroy(healthBar);
        }
    }

    void Death()
    {
        deathScreen.SetActive(true);
        disappointmentTxt.SetActive(true);
        deathScreen.GetComponent<Animator>().SetBool("dead", true);
        isDead = true;
        Time.timeScale = 0f;
    }
}
