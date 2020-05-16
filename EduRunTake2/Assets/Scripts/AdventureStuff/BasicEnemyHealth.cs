using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyHealth : MonoBehaviour
{
    public int currHealth, maxHealth;
    private float randX;
    private int randSpawn;
    public GameObject enemyOne, enemyTwo, poof;

    // Start is called before the first frame update
    private void Awake()
    {
        currHealth = maxHealth;
        randX = Random.Range(-4f, 4f);
        randSpawn = Random.Range(1, 3);
    }

    void Start()
    {

    }

    public void TakeDamage(int dmg)
    {
        currHealth -= dmg;

        if(currHealth <= 0)
        {
            Debug.Log(randSpawn);
            //Spawn();
            Instantiate(poof, gameObject.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    public void Spawn()
    {
        if(randSpawn == 1)
        {
            Instantiate(enemyOne, new Vector2(randX, 8), Quaternion.identity);
        }
        else
        {
            Instantiate(enemyTwo, new Vector2(randX, 8), Quaternion.identity);
        }
    }
}
