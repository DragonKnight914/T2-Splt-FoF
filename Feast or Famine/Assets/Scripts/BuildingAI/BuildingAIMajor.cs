using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BuildingAIMajor : MonoBehaviour
{
    Canvas myCanvas;
    Camera mainCamera;
    Life life;
    Building b;


    void Awake()
    {
        myCanvas = GetComponentInChildren<Canvas>();    
        life = GetComponent<Life>();
        b = GetComponent<Building>();
    }
    void Update()
    {
       if(mainCamera != null)
       {
            return;
       }
       else
       {
            mainCamera = FindFirstObjectByType<Camera>();
            myCanvas.worldCamera = mainCamera;
       }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (b.Placed)
        {
            if (collision.gameObject.tag == "enemies")
            {
                Debug.Log("LoseLife");
                life.LostLife(5);
            }

            //Can Erase if unnecessary
            if (collision.gameObject.tag == "Player")
            {
                                var player = collision.gameObject;
                Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
            }
        }
    }
}
