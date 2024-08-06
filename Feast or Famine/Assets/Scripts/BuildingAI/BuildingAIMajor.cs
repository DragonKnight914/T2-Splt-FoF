using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BuildingAIMajor : MonoBehaviour
{
    Canvas myCanvas;
    Camera mainCamera;
    Life myLife;
    Building b;


    void Awake()
    {
        myCanvas = GetComponentInChildren<Canvas>();    
        myLife = GetComponent<Life>();
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
                myLife.LostLife(5);
            }

            //Can Erase if unnecessary
            if (collision.gameObject.tag == "Player")
            {
                var player = collision.gameObject;
                Collider2D playerCollider = null;
                Collider2D myCollider = GetComponent<Collider2D>();
                if (player != null)
                {
                    playerCollider = player.GetComponent<Collider2D>();
                }
                if (playerCollider != null && myCollider != null)
                {
                    Physics2D.IgnoreCollision(playerCollider, myCollider, true);
                }
            }
        }
    }

}
