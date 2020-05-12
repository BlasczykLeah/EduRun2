using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private float timeBtwnAtk;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetMouseButtonDown(0))
        {
            SAttack();
        }

        if (Input.GetMouseButtonDown(1))
        {
            CAttack();
        }
        */
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
                enemy.GetComponent<BasicEnemyHealth>().TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
