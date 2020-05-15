using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int totalHealth;
    public int currHealth;
    public bool isDead;

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
            collision.gameObject.GetComponent<BasicEnemyHealth>().TakeDamage(999);
        }
    }

    void TakeDamage()
    {
        currHealth--;

        if(currHealth <= 0)
        {
            Destroy(gameObject);
            Destroy(healthBar);
        }
    }

    void Death()
    {
        isDead = true;
    }
}
