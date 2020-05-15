using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeAttack : MonoBehaviour
{
    [Header("Position of where to start the attack hitbox")]
    public Transform attackPos;
    [Header("Radius for the attack hitbox")]
    public float attackRange = 0.5f;
    [Header("Layers that are checked by the attacks")]
    public LayerMask S_enemyLayers, C_enemyLayers, T_enemyLayers;
    [Header("Damage per attack")]
    public int damage;
    [Header("Cooldown between attacks")]
    public float cooldown;

    public int count;
    public GameObject answerCount;
    public Image[] dots;
    public Sprite fullDot, emptyDot;

    private float timeBtwnAtk;
    public Animator anim;

    private void Start()
    {
        count = 0;
    }

    private void Update()
    {
        for (int i = 0; i < dots.Length; i++)
        {
            if (i < count)
            {
                dots[i].enabled = true;
            }
            else
            {
                dots[i].enabled = false;
            }
        }
    }

    public void SAttack()
    {
        if (Time.time > timeBtwnAtk)
        {
            timeBtwnAtk = Time.time + cooldown;

            anim.SetTrigger("Attack");

            Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPos.position, attackRange, S_enemyLayers);

            foreach (Collider2D enemy in enemiesHit)
            {
                Debug.Log("Enemy Hit!");
                count++;
                enemy.GetComponent<BasicEnemyHealth>().TakeDamage(damage);
            }
        }
    }

    public void CAttack()
    {
        if (Time.time > timeBtwnAtk)
        {
            timeBtwnAtk = Time.time + cooldown;

            anim.SetTrigger("Attack");

            Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPos.position, attackRange, C_enemyLayers);

            foreach (Collider2D enemy in enemiesHit)
            {
                Debug.Log("Enemy Hit!");
                count++;
                enemy.GetComponent<BasicEnemyHealth>().TakeDamage(damage);
            }
        }
    }

    public void TAttack()
    {
        if (Time.time > timeBtwnAtk)
        {
            timeBtwnAtk = Time.time + cooldown;

            anim.SetTrigger("Attack");

            Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPos.position, attackRange, T_enemyLayers);

            foreach (Collider2D enemy in enemiesHit)
            {
                Debug.Log("Enemy Hit!");
                count++;
                enemy.GetComponent<BasicEnemyHealth>().TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
