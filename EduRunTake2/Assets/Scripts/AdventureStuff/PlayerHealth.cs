using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int totalHealth;
    private int currHealth;

    void Start()
    {
        currHealth = totalHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
            Destroy(collision.gameObject);
        }
    }

    void TakeDamage()
    {
        if(currHealth <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            currHealth--;
        }
    }
}
