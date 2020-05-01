using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyHealth : MonoBehaviour
{
    public int currHealth, maxHealth;
    private float randX;
    public GameObject Enemy;

    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
        randX = Random.Range(-10f, 10f);
    }

    public void TakeDamage(int dmg)
    {
        currHealth -= dmg;

        if(currHealth <= 0)
        {
            Spawn();
            Destroy(this.gameObject);
        }
    }

    public void Spawn()
    {
        Instantiate(Enemy, new Vector2(randX, 10), Quaternion.identity);
    }
}
