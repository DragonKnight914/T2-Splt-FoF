using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class TDEnemy : MonoBehaviour
{
    public Transform Objective;
    public float Speed = 0.1f;
    public int DamagePerAttack = 20;
    public int AttackCooldownInSeconds = 3;

    private Rigidbody2D rb2d;
    private Health attackTarget;
    private float attackCooldown;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (attackTarget != null)
        {
            TryAttack(attackTarget);
        }
    }

    void FixedUpdate()
    {
        Vector2 towardsObjective = (Objective.position - rb2d.transform.position).normalized;
        rb2d.MovePosition(rb2d.position + towardsObjective * Speed * Time.fixedDeltaTime);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        attackTarget = collision.gameObject.GetComponent<Health>();
    }

    private bool TryAttack(Health target)
    {
        attackCooldown -= Time.deltaTime;
        if (attackCooldown < 0)
        {
            AttackNow(target);
            return true;
        }

        return false;
    }

    private void AttackNow(Health target)
    {
        target.TakeDamage(DamagePerAttack);
        attackCooldown = AttackCooldownInSeconds;
    }
}
