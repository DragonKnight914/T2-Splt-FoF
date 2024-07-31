using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirFan : MonoBehaviour
{
    Rigidbody2D rb;
    float lifetime = 5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * 5f;
        Destroy(gameObject, lifetime);
    }
}
