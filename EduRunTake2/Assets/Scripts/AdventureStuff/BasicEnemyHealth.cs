using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyHealth : MonoBehaviour
{
    public int currHealth, maxHealth;
    private float randX;
    private int randSpawn;
    public GameObject S_Enemy, C_Enemy;

    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
        randX = Random.Range(-4f, 4f);
        randSpawn = Random.Range(1, 3);
    }

    public void TakeDamage(int dmg)
    {
        currHealth -= dmg;

        if(currHealth <= 0)
        {
            Debug.Log(randSpawn);
            Spawn();
            Destroy(this.gameObject);
        }
    }

    public void Spawn()
    {
        if(randSpawn == 1)
        {
            Instantiate(S_Enemy, new Vector2(randX, 8), Quaternion.identity);
        }
        else if(randSpawn == 2)
        {
            Instantiate(C_Enemy, new Vector2(randX, 8), Quaternion.identity);
        }
    }
}
