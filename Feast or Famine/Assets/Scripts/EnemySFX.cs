using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemySFX : MonoBehaviour
{
    public float waitTime = 2.0f; //play sfx every 2 sec?
    float timer = 0.0f;
    AudioSource AnimalAudioSource;
    

    // Start is called before the first frame update
    void Start()
    {
        AnimalAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= waitTime)
        {
            AnimalAudioSource.Play();
            timer = 0.0f;
        }
    }
}
