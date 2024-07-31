using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanMode : BuildingAIMajor
{
    [SerializeField] GameObject airFanPrefab;
    [SerializeField] Transform airPlacement;
    [SerializeField] Transform airPlacement2;

    [SerializeField] float airTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    public void Shoot()
    {
        airTime -= Time.deltaTime;

        if (airTime <= 0f)
        {
            GameObject air1 = Instantiate(airFanPrefab, airPlacement.position, Quaternion.identity);
            GameObject air2 = Instantiate(airFanPrefab, -airPlacement2.position, Quaternion.identity);
            Debug.Log("instantiated air");

            airTime = 5f;
        }
    }
}
