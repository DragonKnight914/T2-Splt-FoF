using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : MonoBehaviour
{
    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
        health.OnNoHealthRemaining += NoHealthRemaining;
    }

    void Start()
    {
        
    }

    void Update()
    {

    }

    private void NoHealthRemaining(Health hp)
    {
        Destroy(gameObject);
    }
}
