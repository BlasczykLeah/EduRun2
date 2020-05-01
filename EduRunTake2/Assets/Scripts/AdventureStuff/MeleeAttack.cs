using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public Transform attackPos;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        anim.SetTrigger("Attack");

        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in enemiesHit)
        {
            Debug.Log("Enemy Hit!");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
