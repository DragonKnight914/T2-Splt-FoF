using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform EnemyTargetObjective;
    public int SecondsBetweenWaves = 30;
    public EnemyWave[] Waves;

    private float waveCooldown;
    private EnemyWave currentWave;


    void Start()
    {
        foreach (EnemyWave wave in Waves)
        {
            wave.SetObjective(EnemyTargetObjective);
        }

        currentWave = Waves.FirstOrDefault();
    }

    void Update()
    {
        currentWave.Update(Time.deltaTime);
    }
}

[Serializable]
public class EnemyWave
{
    public TDEnemy Enemy;
    public Transform[] SpawnLocations;
    public int NumberOfEnemies = 10;
    public int SecondsBetweenSpawns = 3;

    public bool Complete { get; private set; } = false;

    private Transform target;
    private float cooldown;
    private int enemiesSpawned;

    public void SetObjective(Transform objective)
    {
        target = objective;
    }

    public void Update(float deltaTime)
    {
        if (Complete)
            return;

        if (cooldown <= 0)
        {
            cooldown = SecondsBetweenSpawns;
            SpawnEnemies();
        }
    }

    private void SpawnEnemies()
    {
        foreach (var spawnLocation in SpawnLocations)
        {
            if (enemiesSpawned >= NumberOfEnemies)
            {
                Complete = true;
                break;
            }

            var enemy = GameObject.Instantiate(Enemy, spawnLocation, spawnLocation);
            enemiesSpawned++;

            enemy.Objective = target;
            enemy.transform.position = spawnLocation.position;
        }
    }
}