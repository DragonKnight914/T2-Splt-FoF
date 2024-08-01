using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTower : BuildingAIMajor
{
    [SerializeField] GameObject target;
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] Transform arrowPlacement;

    [SerializeField] float arrowTimer = 5f;

    Building b;

    public float radius = 5f;
    Vector2 centerPos;

    private void Start()
    {
        b = GetComponent<Building>();   
    }

    void Update()
    {
        centerPos = transform.position;

        if(target != null && b.Placed)
        {
            Shoot();
        }
        else
        {
            target = FoundCloserObject(centerPos, radius);
        }
    }

    //Algorithm to find the Closer GameObject
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


    public void Shoot()
    {
        arrowTimer -= Time.deltaTime;

        if(arrowTimer <= 0f)
        {
            GameObject arrow = Instantiate(arrowPrefab, arrowPlacement.position, Quaternion.identity);
            Debug.Log("instantiated");
            Vector3 dir = (target.transform.position - transform.position).normalized;

            float angleArrow = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleArrow));

            arrowTimer = 5f;
        }
    }


    //Debug closer Enemies
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
