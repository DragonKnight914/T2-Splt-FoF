using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanMode : BuildingAIMajor
{
    [SerializeField] GameObject airFanPrefab;
    [SerializeField] GameObject target;
    [SerializeField] Transform airPlacement;

    Building b;


    [SerializeField] float airTime = 5f;
    
    public float radius = 5f;
    Vector2 centerPos;

    // Start is called before the first frame update
    void Start()
    {
        b = GetComponent<Building>();
    }

    // Update is called once per frame
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

    GameObject FoundCloserObject(Vector2 centerPos, float radius)
    {
        Collider2D[] objectsInsideRadius = Physics2D.OverlapCircleAll(centerPos, radius);
        GameObject closerObj = null;

        float distanceClosier = Mathf.Infinity;

        foreach (Collider2D col in objectsInsideRadius)
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
        airTime -= Time.deltaTime;

        if (airTime <= 0f)
        {
            GameObject air = Instantiate(airFanPrefab, airPlacement.position, Quaternion.identity);
            Debug.Log("instantiated air");

            Vector3 dir = (target.transform.position - transform.position).normalized;
            float angleAir = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            air.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleAir));

            airTime = 5f;
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
