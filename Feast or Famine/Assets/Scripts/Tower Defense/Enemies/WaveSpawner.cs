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
    public BuildTime bt;
    public List<EnemyWave> Waves;
    public bool AllWavesSpawned { get; private set; } = false;

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
        if (bt.canBuild == false)
        {
                if (AllWavesSpawned)
                {
                    return;
                }

                if (!currentWave.Complete)
                {
                    // The current wave is active and can spawn enemies
                    currentWave.Update(Time.deltaTime);
                }
                else
                {
                    // Wave is done spawning enemies, now wait till creating the next wave
                    waveCooldown -= Time.deltaTime;

                    if (waveCooldown < 0)
                    {
                        waveCooldown = SecondsBetweenWaves;

                        // Select the next wave
                        Waves.Remove(currentWave);
                        currentWave = Waves.FirstOrDefault();

                        if (currentWave == null)
                        {
                            AllWavesSpawned = true;
                        }
                    }
                }
        }
        else if (bt.canBuild)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemies");
            for (int j = 0; j < enemies.Length; j++)
            {
                Destroy(enemies[j]);
            }
        }
    }
    
}


[Serializable]
public class EnemyWave
{
    public List<EnemyWaveSpawnCount> EnemiesInWave;
    public int SecondsBetweenSpawns = 3;

    public bool Complete { get; private set; } = false;

    private Transform target;
    private float cooldown;

    public void SetObjective(Transform objective)
    {
        target = objective;
    }

    public void Update(float deltaTime)
    {
        if (Complete)
            return;

        cooldown -= deltaTime;
        if (cooldown <= 0)
        {
            cooldown = SecondsBetweenSpawns;
            SpawnEnemies();
        }
    }

    private void SpawnEnemies()
    {
        foreach (var enemy in EnemiesInWave)
        {
            if (!enemy.Complete)
            {
                enemy.SpawnEnemy(target);
            }
        }

        Complete = EnemiesInWave.All(x => x.Complete);
    }
}

[Serializable]
public class EnemyWaveSpawnCount
{
    public TDEnemy Enemy;
    public Transform SpawnLocation;
    public int NumberOfEnemies = 10;

    public bool Complete { get; private set; } = false;

    private int enemiesSpawned;

    public TDEnemy SpawnEnemy(Transform target)
    {
        var enemy = GameObject.Instantiate(Enemy, SpawnLocation, SpawnLocation);
        enemiesSpawned++;

        enemy.Objective = target;
        enemy.transform.position = SpawnLocation.position;

        if (enemiesSpawned >= NumberOfEnemies)
        {
            Complete = true;
        }

        return enemy;
    }
}