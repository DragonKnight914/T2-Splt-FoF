using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemySFX : MonoBehaviour
{
    public float minDelay = 2.0f; //play sfx every 2 sec?
    public float maxDelay = 10f;
    public float minPitch = 0.75f;
    public float maxPitch = 1.33f;
    float timer = 0.0f;
    public AudioSource AnimalAudioSource;
    

    // Start is called before the first frame update
    void Start()
    {
        AnimalAudioSource = GetComponent<AudioSource>();
        timer = Time.time + Random.Range(minDelay, maxDelay);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= timer)
        {
            AnimalAudioSource.pitch = Random.Range(minPitch, maxPitch);
            AnimalAudioSource.Play();
            timer = Time.time + Random.Range(minDelay, maxDelay);
        }
    }
}
