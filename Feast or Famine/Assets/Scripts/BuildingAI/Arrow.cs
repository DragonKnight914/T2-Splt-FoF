using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D rb;
    float lifetime = 5f;
    public int arrowDamage = 2;
    public float arrowSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * arrowSpeed;
        Destroy(gameObject, lifetime);
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemies")
        {
            //Temporary
            collision.gameObject.GetComponent<TDEnemy>().Damage(arrowDamage);
            Destroy(this.gameObject);
        }
    }
}
