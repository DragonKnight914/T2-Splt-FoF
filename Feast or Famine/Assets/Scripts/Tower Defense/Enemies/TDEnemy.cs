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
    public int health = 5;

    //Animator
    private Animator anim;

    private Rigidbody2D rb2d;
    private Life attackTarget;
    private float attackCooldown;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        DamagePerAttack *= PlayerPrefs.GetInt("RoundScaling");
        health *= PlayerPrefs.GetInt("RoundScaling");
        Speed *= ((float)PlayerPrefs.GetInt("RoundScaling"));
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
        if (collision.gameObject.CompareTag("Food"))
        {
            CurrentScore score = GameObject.Find("Score").GetComponent<CurrentScore>();
            int newScore = PlayerPrefs.GetInt("Resources");
            newScore -= DamagePerAttack;
            PlayerPrefs.SetInt("Resources", newScore);
            anim.SetTrigger("Death");
            Destroy(this.gameObject, anim.GetCurrentAnimatorStateInfo(0).length);
        }
        else
            attackTarget = collision.gameObject.GetComponent<Life>();
    }

    private bool TryAttack(Life target)
    {
        attackCooldown -= Time.deltaTime;
        if (attackCooldown < 0)
        {
            AttackNow(target);
            return true;
        }

        return false;
    }

    private void AttackNow(Life target)
    {
        target.LostLife(DamagePerAttack);
        attackCooldown = AttackCooldownInSeconds;
    }

    public void Damage(int damageValue)
    {
        health -= damageValue;
        if (health <= 0)
        {

            anim.SetTrigger("Death");
            Destroy(this.gameObject, anim.GetCurrentAnimatorStateInfo(0).length);
        }
    }
}
