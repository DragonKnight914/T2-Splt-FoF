using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Arrow : MonoBehaviour
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


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemies")
        {
            //Temporary
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
