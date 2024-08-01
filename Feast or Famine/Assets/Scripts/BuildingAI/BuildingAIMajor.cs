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
                life.LostLife(5);
            }
        }

    }
}
