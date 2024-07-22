using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int MaxHealth = 100;
    public float CurrentHealth {get; private set;}

    public event Action<Health, float> OnDamageTaken;
    public event Action<Health> OnNoHealthRemaining;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;

        //Debug.Log($"{name} took {damage} damage, current hp {CurrentHealth}");

        OnDamageTaken?.Invoke(this, damage);

        if (CurrentHealth <= 0)
        {
            OnNoHealthRemaining?.Invoke(this);
        }
    }
}
