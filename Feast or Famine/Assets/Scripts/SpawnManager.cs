using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject flyingFish = null;
    [SerializeField] private GameObject highFlyingFish = null;
    [SerializeField] private GameObject swimmingFish = null;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DefaultFishSpawnRoutine());
        StartCoroutine(HighFishSpawnRoutine());
        StartCoroutine(SwimmingFishSpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DefaultFishSpawnRoutine()
    {
        while(true)
        {
            float randomTime = Random.Range(3.0f, 10.0f);
            Instantiate(flyingFish, new Vector3(Random.Range(-2740f, -2715f), Random.Range(568f, 650f), transform.position.z), Quaternion.identity);
            yield return new WaitForSeconds(randomTime);
        }
    }

    IEnumerator HighFishSpawnRoutine()
    {
        while(true)
        {
            float randomTime = Random.Range(3.0f, 10.0f);
            Instantiate(highFlyingFish, new Vector3(Random.Range(-2740f, -2715f), Random.Range(650f, 750f), transform.position.z), Quaternion.identity);
            yield return new WaitForSeconds(randomTime);
        }
    }

    IEnumerator SwimmingFishSpawnRoutine()
    {
        while(true)
        {
            float randomTime = Random.Range(3.0f, 10.0f);
            Instantiate(swimmingFish, new Vector3(Random.Range(-2740f, -2715f), 563.8f, transform.position.z), Quaternion.identity);
            yield return new WaitForSeconds(randomTime);
        }
    }
}
