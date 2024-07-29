using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTower : BuildingAIMajor
{
    GameObject target;
    float arrowCadency;
    float arrowSpeed;
    bool isThrowing;
    Vector2 centerPos;

    public float radius = 5f;

    // Update is called once per frame
    void Update()
    {
        centerPos = transform.position;

        target = FoundCloserObject(centerPos, radius);
    }

    GameObject FoundCloserObject(Vector2 centerPos, float radius)
    {
        Collider2D[] objectsInsideRadius = Physics2D.OverlapCircleAll(centerPos, radius);
        GameObject closerObj = null;

        float distanceClosier = Mathf.Infinity;

        foreach(Collider2D col in objectsInsideRadius)
        {
            if (col.CompareTag("enemies"))
            {
                float actualDistance = Vector2.Distance(centerPos, col.gameObject.transform.position);
                if (actualDistance < distanceClosier)
                {
                    distanceClosier = actualDistance;
                    closerObj = col.gameObject;
                }
            }
        }

        return closerObj;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);


        if(target != null)    
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, target.transform.position);
        }
    }


}
